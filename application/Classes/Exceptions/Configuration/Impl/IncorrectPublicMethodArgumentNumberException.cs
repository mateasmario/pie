/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

namespace pie.Classes.Exceptions
{
    public class InvalidPluginException : ConfigurationException
    {
        public InvalidPluginException(string message) : base(message)
        {
        }
    }
}
