using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Starbase.CaliberRM.Interop;

namespace EAAddIn
{
    /// <summary>
    /// Singleton used to provide access to CaliberRM Session objects
    /// </summary>
    public class SessionManager
    {
        private static SessionManager instance = new SessionManager();

        private ISession caliberSession;

        /// <summary>
        /// Cached Dictionary of projects keyed on project id
        /// </summary>
        private Dictionary<int, IProject> idProjectDictionary;


        private SessionManager()
        {
            // Initialise Caliber Environment
            Initializer initializer;
            IStJavaVMInfo javaVMinfo;

            initializer = new InitializerClass();
            javaVMinfo = initializer.JavaConfiguration.CurrentJavaVM;
            javaVMinfo.Options = "Xmx1000M";
        }

        /// <summary>
        /// Returns the SessionManager instance
        /// </summary>
        public static SessionManager Object
        {
            get
            {
                if (instance == null)
                {
                    instance = new SessionManager();
                }
                return instance;
            }
        }

        public ISession Session
        {
            get { return caliberSession; }
        }

        public ISession CreateCaliberSession(String host, String userName, String password)
        {
            ICaliberServerFactory serverFactory;
            ICaliberServer server;
            ISession session;

            serverFactory = new CaliberServerFactoryClass();
            server = serverFactory.Create(host);
            session = server.login(userName, password);

            caliberSession = session;

            return session;
        }


        /// <summary>
        /// Finds a project based on its ID. NOTE this method caches a list of projects!
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        public bool FindProject(int projectID, out IProject project)
        {
            if (idProjectDictionary == null)
            {
                idProjectDictionary = Session.Projects.Cast<IProject>().ToDictionary(x => x.ProjectID.IDNumber);
            }

            return idProjectDictionary.TryGetValue(projectID, out project);
        }

        /// <summary>
        /// Finds a req type in the repository
        /// </summary>
        /// <param name="project"></param>
        /// <param name="reqTypeName"></param>
        /// <returns></returns>
        public IRequirementType FindRequirementType(String reqTypeName)
        {
            Collection reqTypes = caliberSession.RequirementTypes;
            //IEnumerable<IRequirementType> results = from IRequirementType r in reqTypes where r.Name == reqTypeName select r;

            //if (results.Count() > 0)
            //{
            //    return results.First();
            //}
            //else
            //{
            return null;
            //}
        }

        /// <summary>
        /// Given a CaliberObjectID returns the associated req object
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        internal IRequirement FetchRequirement(CaliberObjectID id)
        {
            Debug.Assert(id != null);
            return (IRequirement) caliberSession.get(id);
        }
    }
}