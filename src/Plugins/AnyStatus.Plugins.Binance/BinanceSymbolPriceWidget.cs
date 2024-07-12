using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AnyStatus.API.Attributes;
using AnyStatus.API.Widgets;

namespace AnyStatus.Plugins.Binance;

[Category("Binance")]
[DisplayName("Binance Symbol Price")]
[Description("Latest price for a symbol")]
public class BinanceSymbolPriceWidget : TextWidget, IPollable, ICommonWidget
{
    [Required]
    [AsyncItemsSource(typeof(BinanceSymbolSource), true)]
    public string Symbol { get; set; }
}