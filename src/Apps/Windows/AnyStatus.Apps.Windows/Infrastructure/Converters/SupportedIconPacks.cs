using System;
using System.Collections.Generic;
using MahApps.Metro.IconPacks;

namespace AnyStatus.Apps.Windows.Infrastructure.Converters;

internal class SupportedIconPacks
{
    internal static readonly Dictionary<string, Type> IconPacks = new()
                                                                  {
                                                                      { "Material"      , typeof(PackIconMaterialKind) }
                                                                    , { "MaterialLight" , typeof(PackIconMaterialLightKind) }
                                                                    , { "BootstrapIcons", typeof(PackIconBootstrapIconsKind) }
                                                                  };
}