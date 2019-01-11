using System;
using System.Collections.Generic;

namespace UniqueColor.Core.Actions
{
    public class NavigateAction : BaseAction
    {
        public NavigateAction(string path, Dictionary<string, object> parameters = null)
        {
            this.Parameters = parameters ?? new Dictionary<string, object>();
            this.Path = path;
        }

        public string Path { get; set; }

        public bool SetMainPage { get; set; }

        public Dictionary<string, object> Parameters { get; set; }
    }

    public class NavigateAction<T> : BaseAction
    {
        public NavigateAction()
        {

        }

        public T Type { get; set; }
    }
}
