using Ninject;
using System;

namespace HouseholdManager.Web.App_Start
{
    internal sealed class NinjectKernelInstanceProvider
    {
        private static volatile IKernel instance;
        private static object syncRoot = new Object();

        private NinjectKernelInstanceProvider()
        {

        }

        internal static IKernel Instance
        {
            get
            {
                return NinjectKernelInstanceProvider.instance;
            }

            set
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            NinjectKernelInstanceProvider.instance = value;
                        }
                    }
                }
            }
        }
    }
}