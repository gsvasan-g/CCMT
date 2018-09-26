/*
   CreatePrograms.cs

   Marketo REST API Sample Code
   Copyright (C) 2016 Marketo, Inc.

   This software may be modified and distributed under the terms
   of the MIT license.  See the LICENSE file for details.
*/

namespace CCMarketoModels
{

    public partial class CreateProgram
    {
        public class Program1
        {
            public string name { get; set; }
            public string folder { get; set; }
            public string description { get; set; }
            public string type { get; set; }
            public string channel { get; set; }
            public string costs { get; set; }
        }
    }
}