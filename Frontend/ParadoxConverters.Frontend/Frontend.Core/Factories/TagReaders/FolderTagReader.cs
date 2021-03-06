﻿using System.Collections.Generic;
using System.Xml.Linq;
using Caliburn.Micro;
using Frontend.Core.Common.Proxies;
using Frontend.Core.Helpers;
using Frontend.Core.Model.Paths;
using Frontend.Core.Model.Paths.Interfaces;

namespace Frontend.Core.Factories.TagReaders
{
    public class FolderTagReader : TagReaderBase
    {
        public FolderTagReader(IEventAggregator eventAggregator)
            : base(eventAggregator, new DirectoryHelper(), new EnvironmentProxy())
        {
        }

        public IRequiredFolder ReadFolder(XElement xmlElement)
        {
            var directoryTagName = XElementHelper.ReadStringValue(xmlElement, "tag", false);
            var internalTagName = XElementHelper.ReadStringValue(xmlElement, "internalTag", false);

            // TODO: Sanity check
            //if (directoryTagName == null && internalTagName == null)
            //{
            //    this.EventAggregator.PublishOnUIThread(new LogEntry("Invalid XML - no tag or internal tag found for item "))
            //}

            var friendlyName = XElementHelper.ReadStringValue(xmlElement, "friendlyName");
            var description = XElementHelper.ReadStringValue(xmlElement, "description");
            var isMandatory = XElementHelper.ReadBoolValue(xmlElement, "isMandatory", false);
            var hidden = XElementHelper.ReadBoolValue(xmlElement, "hidden", false);
            var alternativePaths = ReadDefaultLocationPaths(xmlElement, directoryTagName, friendlyName);

            //var installationPath = string.IsNullOrEmpty(steamId) ? this.ReadWindowsUserFolderPath(xmlElement) : this.ReadSteamPath(xmlElement, steamId);
            //var installationPath = this.ReadDefaultLocationPaths(xmlElement);

            return BuildRequiredFolderObject(directoryTagName, alternativePaths, friendlyName, description,
                internalTagName, isMandatory, hidden);
        }

        /// <summary>
        ///     Todo: Move this to a separate factory somehow.
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        private IRequiredFolder BuildRequiredFolderObject(string tagName, IList<IAlternativePath> alternatives,
            string friendlyName, string description, string internalTagName, bool isMandatory, bool hidden)
        {
            return new RequiredFolder(tagName, friendlyName, description, alternatives, internalTagName, isMandatory, hidden);
        }
    }
}