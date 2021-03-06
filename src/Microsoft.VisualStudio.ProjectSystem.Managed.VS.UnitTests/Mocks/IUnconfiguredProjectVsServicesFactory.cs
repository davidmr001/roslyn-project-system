﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;
using Microsoft.VisualStudio.Shell.Interop;
using Moq;

namespace Microsoft.VisualStudio.ProjectSystem.VS
{
    internal static class IUnconfiguredProjectVsServicesFactory
    {
        public static IUnconfiguredProjectVsServices Create()
        {
            return Mock.Of<IUnconfiguredProjectVsServices>();
        }

        public static IUnconfiguredProjectVsServices Implement(Func<IVsHierarchy> hierarchyCreator, Func<IVsProject4> projectCreator = null)
        {
            var mock = new Mock<IUnconfiguredProjectVsServices>();
            mock.SetupGet(h => h.VsHierarchy)
                .Returns(hierarchyCreator);

            mock.SetupGet(h => h.ThreadingService)
                .Returns(IProjectThreadingServiceFactory.Create());

            if (projectCreator != null)
            {
                mock.SetupGet(h => h.VsProject)
                    .Returns(projectCreator());
            }

            return mock.Object;
        }
    }
}
