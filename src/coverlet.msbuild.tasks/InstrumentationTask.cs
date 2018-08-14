﻿using System;
using Coverlet.Core;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Coverlet.MSbuild.Tasks
{
    public class InstrumentationTask : Task
    {
        private static Coverage _coverage;
        private string _path;
        private string _exclude;
        private string _include;
        private string _excludeByFile;

        internal static Coverage Coverage
        {
            get { return _coverage; }
        }

        [Required]
        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }
        
        public string Exclude
        {
            get { return _exclude; }
            set { _exclude = value; }
        }

        public string Include
        {
            get { return _include; }
            set { _include = value; }
        }

        public string ExcludeByFile
        {
            get { return _excludeByFile; }
            set { _excludeByFile = value; }
        }

        public override bool Execute()
        {
            try
            {
                var excludedSourceFiles = _excludeByFile?.Split(',');
                var excludeFilters = _exclude?.Split(',');
                var includeFilters = _include?.Split(',');

                _coverage = new Coverage(_path, excludeFilters, includeFilters, excludedSourceFiles, true);
                _coverage.PrepareModules();
            }
            catch (Exception ex)
            {
                Log.LogErrorFromException(ex);
                return false;
            }

            return true;
        }
    }
}
