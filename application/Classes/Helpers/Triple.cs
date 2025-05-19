/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

namespace pie.Classes
{
    public class Triple<A, B, C>
    {
        public A a { get; set; }
        public B b { get; set; }
        public C c { get; set; }

        public Triple(A a, B b, C c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }
    }
}
