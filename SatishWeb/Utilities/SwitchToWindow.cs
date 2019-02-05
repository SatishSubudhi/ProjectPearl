using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PearlFramework;

namespace PearlFramework.Utilities
{
    class SwitchWindow
    {
        protected Boolean switchWindow(String title)
        {
            int i = 0;
            String currentWindow = Driver.Instance.CurrentWindowHandle;
            IReadOnlyCollection<String> availableWindows = Driver.Instance.WindowHandles;
            if (availableWindows.Count > 0)
            {
                while (i <= availableWindows.Count)
                {
                    String windowId = availableWindows.ElementAt(i);
                    if (Driver.Instance.SwitchTo().Window(windowId).Title.Equals(title))
                    {
                        return true;
                    }
                    else
                    {
                        Driver.Instance.SwitchTo().Window(currentWindow);
                    }
                }
            }
            return false;
        }
    }
}

