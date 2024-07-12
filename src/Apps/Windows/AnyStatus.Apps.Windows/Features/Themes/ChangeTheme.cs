using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using AnyStatus.Core.App;
using MediatR;
using ModernWpf;
using ModernWpf.Controls;

namespace AnyStatus.Apps.Windows.Features.Themes;

public sealed class ChangeTheme
{
    public class Request : IRequest<bool>
    {
        public Request(string themeName) { ThemeName = themeName; }

        public string ThemeName { get; }
    }

    public class Handler : RequestHandler<Request, bool>
    {
        private static readonly Color        DefaultAccentColor = Color.FromRgb(0x00, 0x78, 0xD7);
        private readonly        IAppSettings _settings;

        public Handler(IAppSettings settings) { _settings = settings; }

        protected override bool Handle(Request request)
        {
            if (Application.Current.Resources.MergedDictionaries.Count > 0)
            {
                var mergedDictionaries = Application.Current.Resources.MergedDictionaries;
                for (var i = mergedDictionaries.Count - 1; i >= 0; i--)
                {
                    if (mergedDictionaries[i] is not (ThemeResources or XamlControlsResources))
                    {
                        mergedDictionaries.RemoveAt(i);
                    }
                }

                ThemeManager.Current.AccentColor      = DefaultAccentColor;
                ThemeManager.Current.ApplicationTheme = request.ThemeName.Equals("light", StringComparison.CurrentCultureIgnoreCase)
                                                            ? ApplicationTheme.Light
                                                            : ApplicationTheme.Dark;
            }
            else
            {
                var themeResources = new ThemeResources
                                     {
                                         RequestedTheme = request.ThemeName.Equals("light", StringComparison.CurrentCultureIgnoreCase)
                                                              ? ApplicationTheme.Light
                                                              : ApplicationTheme.Dark
                                     };

                themeResources.BeginInit();

                Application.Current.Resources.MergedDictionaries.Add(themeResources);

                themeResources.EndInit();

                Application.Current.Resources.MergedDictionaries.Add(new XamlControlsResources());
            }

            var resources = new List<string>();

            resources.AddRange(_settings.Resources);
            resources.Add("""Resources\Icons\Icons.xaml""");
            resources.Add("""Resources\Styles\DataTemplates.xaml""");
            resources.Add("""Resources\Styles\Style.xaml""");
            resources.Add(request.ThemeName.Equals("light", StringComparison.CurrentCultureIgnoreCase)
                              ? """Features\Themes\Light.xaml"""
                              : """Features\Themes\Dark.xaml""");

            var resourceDictionaries = resources.Select(static resource => new ResourceDictionary
                                                                           {
                                                                               Source = new (resource, UriKind.Relative)
                                                                           });

            foreach (var rd in resourceDictionaries)
            {
                rd.BeginInit();

                Application.Current.Resources.MergedDictionaries.Add(rd);

                rd.EndInit();
            }

            return true;
        }
    }
}