using Ardalis.SmartEnum;
using Ardalis.SmartEnum.SystemTextJson;
using System.Text.Json.Serialization;

namespace MobyLabWebProgramming.Core.Enums;


[JsonConverter(typeof(SmartEnumNameConverter<ProductTagEnum, string>))]
public sealed class ProductTagEnum : SmartEnum<ProductTagEnum, string>
{
    public static readonly ProductTagEnum Scaun = new(nameof(Scaun), "Scaun");
    public static readonly ProductTagEnum Masa = new(nameof(Masa), "Masa");
    public static readonly ProductTagEnum Dulap = new(nameof(Dulap), "Dulap");
    public static readonly ProductTagEnum LemnPal = new(nameof(LemnPal), "LemnPal");
    public static readonly ProductTagEnum LemnCastan = new(nameof(LemnCastan), "LemnCastan");
    public static readonly ProductTagEnum LemnMesteacan = new(nameof(LemnMesteacan), "LemnMesteacan");

    private ProductTagEnum(string name, string value) : base(name, value)
    {
    }
}