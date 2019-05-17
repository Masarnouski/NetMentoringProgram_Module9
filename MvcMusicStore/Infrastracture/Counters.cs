using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PerformanceCounterHelper;

namespace MvcMusicStore.Infrastracture
{
    [PerformanceCounterCategory
        ("MvcMusicStore",
            System.Diagnostics.PerformanceCounterCategoryType.MultiInstance,
            "MvcMusicStore")]
    public enum Counters
    {
        [PerformanceCounter ("Succesfull Log in count", "Log in count", System.Diagnostics.PerformanceCounterType.NumberOfItems32)]
        LogIn,
        [PerformanceCounter ("Succesfull Log out count", "Log out count", System.Diagnostics.PerformanceCounterType.NumberOfItems32)]
        LogOff,
        [PerformanceCounter("Failed Log in count", "Failed Log in count", System.Diagnostics.PerformanceCounterType.NumberOfItems32)]
        FailedLogIn
    }
}