﻿namespace BlogSystem.Web.Infrastructure
{
    using System;
    using System.Linq;
    using System.Collections.Generic;   
    using BlogSystem.Data.Models;
    using BlogSystem.Data.Repositories;

    public class SettingsManager : ISettingsManager
    {
        private readonly IDbRepository<Setting> settingsRepository;
        private Lazy<IDictionary<string, string>> settings;

        public SettingsManager(IDbRepository<Setting> settingsRepository)
        {
            this.settingsRepository = settingsRepository;
        }

        public IDictionary<string, string> GetSettings()
        {
            Func<IDictionary<string, string>> getSettings = delegate()
            {
                return this.settingsRepository.All().ToDictionary(s => s.Key, s => s.Value);
            };

            this.settings = new Lazy<IDictionary<string, string>>(getSettings);

            return this.settings.Value;
        }

        public string this[string key] => this.settings.Value[key];
    }
}