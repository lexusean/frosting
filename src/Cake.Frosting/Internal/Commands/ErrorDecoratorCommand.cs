﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Cake.Core;

namespace Cake.Frosting.Internal.Commands
{
    internal sealed class ErrorDecoratorCommand : Command
    {
        private readonly Command _command;

        public ErrorDecoratorCommand(Command command)
        {
            _command = command;
        }

        public override bool Execute(ICakeEngine engine, CakeHostOptions options)
        {
            _command.Execute(engine, options);
            return false;
        }
    }
}
