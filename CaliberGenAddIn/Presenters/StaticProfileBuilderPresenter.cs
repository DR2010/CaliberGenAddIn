using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAAddIn.Applications.StaticProfileBuilder;
using EAAddIn.Windows.Interfaces;

namespace EAAddIn.Presenters
{
    public class StaticProfileBuilderPresenter
    {
        public StaticProfileBuilderPresenter(StaticProfileBuilder model, IStaticProfileBuilder view)
        {
            Model = model;
            View = view;

            View.Show();
            View.Diagram = Model.Diagram;
        }

        public StaticProfileBuilder Model
        {
            get; set;
        }
        public IStaticProfileBuilder View
        {
            get; set;
        }

    }
}
