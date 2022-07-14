using System;
using System.Collections.Generic;
using System.Diagnostics;
using Starbase.CaliberRM.Interop;

namespace EAAddIn
{
    /// <summary>
    /// A wrapper for the CaliberRM Requirement class that provides copying and comparison functionality.
    /// Also maintains a dictionary of the requirements user defined attributes for fast access.
    /// </summary>
    public class ExtendedRequirement
    {
        // Names of fields used for merging
        internal const string BRANCH_BASE_VERSION = "ZZZZ-BRANCH-BASE-VERSION";
        internal const string REQUIREMENT_LOCKED = "ZZZZ-LOCKED";
        internal const string SOURCE_PROJECT_ATTRIBUTE = "ZZZZ-SOURCE-PROJECT";
        internal const string SOURCE_REQUIREMENT_ID_ATTRIBUTE = "ZZZZ-SOURCE-REQUIREMENT-ID";
        internal const string SOURCE_VERSION_ATTRIBUTE = "ZZZZ-SOURCE-REQUIREMENT-VERSION";

        private static readonly List<string> mergeAttributes = new List<string>(5);

        // Reference to the attached requirement

        // Dictionary of requirement attributes (keyed by name)
        private IDictionary<string, IAttributeValue> attributeDictionary;
        private IRequirement requirement;

        static ExtendedRequirement()
        {
            mergeAttributes.Add(SOURCE_PROJECT_ATTRIBUTE);
            mergeAttributes.Add(SOURCE_REQUIREMENT_ID_ATTRIBUTE);
            mergeAttributes.Add(SOURCE_VERSION_ATTRIBUTE);
            mergeAttributes.Add(BRANCH_BASE_VERSION);
            mergeAttributes.Add(REQUIREMENT_LOCKED);
        }

        internal ExtendedRequirement(IRequirement requirement)
        {
            this.requirement = requirement;
        }

        public IRequirement Requirement
        {
            get { return requirement; }
        }

        /// <summary>
        /// Retrieves the value of the named UDA
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal IAttributeValue this[string name]
        {
            get
            {
                if (attributeDictionary == null)
                {
                    populateAttributeDictionary();
                }
                return attributeDictionary[name];
            }
        }

        private IDictionary<string, IAttributeValue> AttributeDictionary
        {
            get
            {
                if (attributeDictionary == null)
                {
                    populateAttributeDictionary();
                }

                return attributeDictionary;
            }
        }

        /// <summary>
        /// Checks to see if the requirement is locked
        /// </summary>
        internal bool Locked
        {
            get
            {
                var locked = (IUDABooleanValue) this[REQUIREMENT_LOCKED];
                return locked.Value;
            }
            set
            {
                var locked = (IUDABooleanValue) this[REQUIREMENT_LOCKED];
                locked.Value = value;
            }
        }

        /// <summary>
        /// Tests to see if this requirement is "new", that is created in a branched project
        /// </summary>
        /// <returns></returns>
        internal bool IsNew
        {
            get
            {
                // A new reuirement will not have a valid value in SOURCE_REQUIREMENT_ID_ATTRIBUTE
                String sourceRequirementId = ((IUDATextValue) this[SOURCE_REQUIREMENT_ID_ATTRIBUTE]).Value;
                int id;
                return !Int32.TryParse(sourceRequirementId, out id);
            }
        }

        /// <summary>
        /// Returns this requirements source requirement ID. Only valid if the requirement is not new
        /// </summary>
        internal int SourceRequirementID
        {
            get
            {
                String sourceRequirementId = ((IUDATextValue) this[SOURCE_REQUIREMENT_ID_ATTRIBUTE]).Value;
                try
                {
                    return Int32.Parse(sourceRequirementId);
                }
                catch
                {
                    return 0;
                }
            }
            set { ((IUDATextValue) this[SOURCE_REQUIREMENT_ID_ATTRIBUTE]).Value = value.ToString(); }
        }

        public String SourceProjectName
        {
            get { return ((IUDATextValue) this[SOURCE_PROJECT_ATTRIBUTE]).Value; }
            set { ((IUDATextValue) this[SOURCE_PROJECT_ATTRIBUTE]).Value = value; }
        }

        public string SourceRequirementVersion
        {
            get { return ((IUDATextValue) AttributeDictionary[SOURCE_VERSION_ATTRIBUTE]).Value; }
            set { ((IUDATextValue) AttributeDictionary[SOURCE_VERSION_ATTRIBUTE]).Value = value; }
        }

        /// <summary>
        /// Populates the dictionary that contains all the UDAS
        /// </summary>
        private void populateAttributeDictionary()
        {
            if (requirement != null)
            {
                attributeDictionary = new Dictionary<string, IAttributeValue>(requirement.AttributeValues.Count);
                foreach (IAttributeValue attributeValue in requirement.AttributeValues)
                {
                    // It is possible to have the same attribute appear multiple times in a 
                    // req but we only have to add once.
                    if (!attributeDictionary.ContainsKey(attributeValue.Attribute.Name))
                        attributeDictionary.Add(attributeValue.Attribute.Name, attributeValue);
                }
            }
        }

        /// <summary>
        /// Copies all properties from the sourse requirement to the requirement represented by this
        /// Object
        /// </summary>
        /// <param name="source"></param>
        internal void Copy(ExtendedRequirement source, bool includeMergeAttributes)
        {
            // First the details tab
            // When copying owner we must first ensure that the owner is assigned to the target project
            if (source.requirement.Owner != null)
            {
                if (Requirement.Project.isUserAssigned(source.Requirement.Owner))
                    Requirement.Owner = source.Requirement.Owner;
            }
            Requirement.Status = source.Requirement.Status;
            Requirement.Priority = source.Requirement.Priority;
            Requirement.Description = source.Requirement.Description;

            // Validation Tab
            Requirement.Validation = source.Requirement.Validation;

            //References
            CopyReferences(source);

            // Now for the UDAs
            Collection sourceAttributes = source.Requirement.AttributeValues;

            for (int i = 0; i < sourceAttributes.Count; i++)
            {
                var sourceAttribute = (IAttributeValue) sourceAttributes[i];
                IAttributeValue targetAttribute = AttributeDictionary[sourceAttribute.Attribute.Name];

                Debug.Assert(sourceAttribute.Attribute.Name.Equals(targetAttribute.Attribute.Name));

                // If attribute is one of the merge attributes then ignore
                if (mergeAttributes.Contains(sourceAttribute.Attribute.Name))
                {
                    continue;
                }


                if (sourceAttribute is IUDAListValue)
                {
                    var sourceListValue = (IUDAListValue) sourceAttribute;
                    var targetListValue = (IUDAListValue) targetAttribute;

                    // We get problems if list entries have beem removed, modification handles these scenarions
                    // corectly
                    if (sourceListValue.FirstHiddenIndex > -1)
                    {
                        Collection selectedIndices = (new CollectionFactoryClass()).Create();
                        foreach (int index in sourceListValue.SelectedIndices)
                        {
                            if (index < sourceListValue.FirstHiddenIndex)
                            {
                                selectedIndices.Add(index);
                            }
                        }
                        targetListValue.SelectedIndices = selectedIndices;
                    }
                    else
                    {
                        targetListValue.SelectedIndices = sourceListValue.SelectedIndices;
                    }
                }

                else if (sourceAttribute is IUDABooleanValue)
                {
                    ((IUDABooleanValue) targetAttribute).Value = ((IUDABooleanValue) sourceAttribute).Value;
                }
                else if (sourceAttribute is IUDADateValue)
                {
                    var sourceDateValue = ((IUDADateValue) sourceAttribute);
                    var targetDateValue = ((IUDADateValue) targetAttribute);

                    Double minimumDate = ((IUDADate) sourceDateValue.Attribute).MinimumOLEDate;
                    Double maximumDate = ((IUDADate) sourceDateValue.Attribute).MaximumOLEDate;

                    if (sourceDateValue.Blank)
                    {
                        targetDateValue.setBlank();
                    }
                        //else if ((sourceDateValue.OLEDate < minimumDate) || (sourceDateValue.OLEDate > maximumDate))
                        //{
                        //    Logger.LogWarning("Requirement ID {0}, attribute {1} contains an out of range date", source.Requirement.IDNumber, sourceDateValue.Attribute.Name);
                        //}
                    else if (targetDateValue.OLEDate != sourceDateValue.OLEDate)
                    {
                        targetDateValue.OLEDate = sourceDateValue.OLEDate;
                    }
                }
                else if (sourceAttribute is IUDATextValue)
                {
                    ((IUDATextValue) targetAttribute).Value = ((IUDATextValue) sourceAttribute).Value;
                }
                else if (sourceAttribute is IUDAFloatValue)
                {
                    ((IUDAFloatValue) targetAttribute).Value = ((IUDAFloatValue) sourceAttribute).Value;
                }
                else if (sourceAttribute is IUDAIntegerValue)
                {
                    ((IUDAIntegerValue) targetAttribute).Value = ((IUDAIntegerValue) sourceAttribute).Value;
                }
                else
                {
                    Debug.Assert(false, "Unsupported attribute type");
                }
                ;
            }

            if (includeMergeAttributes)
            {
                if (AttributeDictionary.ContainsKey(SOURCE_PROJECT_ATTRIBUTE)
                    && AttributeDictionary.ContainsKey(SOURCE_REQUIREMENT_ID_ATTRIBUTE)
                    && AttributeDictionary.ContainsKey(SOURCE_VERSION_ATTRIBUTE)
                    && AttributeDictionary.ContainsKey(BRANCH_BASE_VERSION))
                {
                    SourceProjectName = source.Requirement.Project.Name;
                    SourceRequirementID = source.Requirement.IDNumber;
                    SourceRequirementVersion = source.Requirement.Version.MajorVersion + "." +
                                               source.Requirement.Version.MinorVersion;
                }
                //else
                //{
                //    Logger.LogError("One or more merge attributes are not availible for requirement id {0}" +
                //        " Requirement type '{1}'", source.Requirement.IDNumber, source.Requirement.RequirementType.Name);

                //}
            }
        }

        /// <summary>
        /// Set the Merge attributes that records the current version of the branched requirement.
        /// This field is used in a merge to detect whether a requirement has changed or not.
        /// </summary>
        internal void SetBaseVersionValue()
        {
            Requirement.@lock();

            // As setting this attribute will result in a version change we must "predict" the new version number
            var rev = (IHistoryRevision) Requirement.History.Revisions[Requirement.History.Revisions.Count - 1];

            var branchBaseVersionValue = (IUDATextValue) AttributeDictionary[BRANCH_BASE_VERSION];
            int majorVersion = rev.Version.MajorVersion;
            int minorVersion = rev.Version.MinorVersion;

            // Work out what the new version number will be after the requirement is saved
            if (branchBaseVersionValue.Attribute.TriggersMajorVersionChange)
            {
                majorVersion++;
            }
            else
            {
                minorVersion++;
            }
            branchBaseVersionValue.Value = majorVersion + "." + minorVersion;

            Requirement.save("set merge version attribute");
            Requirement.unlock();
        }

        /// <summary>
        /// Copies references from sourceRequirement to this
        /// </summary>
        /// <param name="sourceRequirement"></param>
        private void CopyReferences(ExtendedRequirement sourceRequirement)
        {
            IRequirement source = sourceRequirement.Requirement;

            if (source.DocumentReferences.Count > 0)
            {
                CollectionFactory colFactory = new CollectionFactoryClass();
                Collection docRefs = colFactory.Create();
                foreach (IDocumentReference docRef in source.DocumentReferences)
                {
                    if (docRef is IFileReference)
                    {
                        var sourceFileRef = (IFileReference) docRef;
                        if (sourceFileRef.Path.Length > sourceFileRef.MAX_PATH_LENGTH)
                        {
                            // Logger.LogWarning("Cannot copy file reference '{0}' from requirement id {1}", sourceFileRef.Path, source.IDNumber);
                        }
                        else
                        {
                            IFileReferenceFactory fact = new FileReferenceFactoryClass();
                            IFileReference fileRef = fact.Create2("", sourceFileRef.Description,
                                                                  sourceFileRef.KeyReference);
                            fileRef.Path = sourceFileRef.Path;
                            docRefs.Add(fileRef);
                        }
                    }
                }
                Requirement.DocumentReferences = docRefs;
            }
        }

        /// <summary>
        /// Tests to see if the requirement has the named attribute
        /// </summary>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        internal bool HasAttribute(string attributeName)
        {
            return AttributeDictionary.ContainsKey(attributeName);
        }

        /// <summary>
        /// Checks to see if this requirement has all the UDA necessary for 
        /// merging\branching
        /// </summary>
        /// <returns></returns>
        internal bool HasAllMergeAttributes()
        {
            foreach (string attributeName in mergeAttributes)
            {
                if (!HasAttribute(attributeName))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Detects whether this requirement has changed since it was branched
        /// </summary>
        /// <returns></returns>
        internal bool ChangedSinceBranch()
        {
            // Only something with all merge attributes could have been branched
            Debug.Assert(HasAllMergeAttributes());

            var branchBaseVersion = (IUDATextValue) this[BRANCH_BASE_VERSION];
            String currentVersion = Requirement.Version.MajorVersion + "." + Requirement.Version.MinorVersion;
            if (currentVersion.Equals(branchBaseVersion.Value))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Copies the traces from the source using the mapping rules defined in sourceTargetMapping. Note that this
        /// method is only used when branching requirements
        /// </summary>
        /// <param name="source"></param>
        /// <param name="sourceTargetMapping"></param>
        internal void CopyTraces(ExtendedRequirement source, Dictionary<int, CaliberObjectID> sourceTargetMapping)
        {
            //Logger.LogInformation("Copying Trace info for " + source.Requirement.IDNumber);
            ITraceManager tm = SessionManager.Object.Session.TraceManager;

            foreach (ITrace trace in source.Requirement.TracesFrom)
            {
                CaliberObject traceFrom = trace.FromObject;
                if (traceFrom is IRequirement)
                {
                    // Check to see if the trace is from a requirement in this project
                    if (sourceTargetMapping.ContainsKey(traceFrom.ID.IDNumber))
                    {
                        traceFrom = SessionManager.Object.Session.get(sourceTargetMapping[trace.TraceFromID.IDNumber]);
                    }

                    tm.createTrace2(traceFrom, (CaliberObject) Requirement, trace.Suspect);
                }
            }

            foreach (ITrace trace in source.Requirement.TracesTo)
            {
                CaliberObject traceTo = trace.ToObject;
                if (traceTo is IRequirement)
                {
                    // Check to see if the trace is from a requirement in this project
                    if (sourceTargetMapping.ContainsKey(trace.TraceToID.IDNumber))
                    {
                        traceTo = SessionManager.Object.Session.get(sourceTargetMapping[trace.TraceToID.IDNumber]);
                    }

                    tm.createTrace2((CaliberObject) Requirement, traceTo, trace.Suspect);
                }
            }

            //this.RefreshFromStorage();

            SetBaseVersionValue();
        }

        /// <summary>
        /// Reloads the wrapped requirement from the database.
        /// </summary>
        public void RefreshFromStorage()
        {
            requirement = (IRequirement) SessionManager.Object.Session.get(requirement.ID);
            attributeDictionary = null;
        }
    }
}