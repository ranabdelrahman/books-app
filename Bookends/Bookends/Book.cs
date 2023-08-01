using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
namespace Bookends
{
    // Book myDeserializedClass = JsonConvert.DeserializeObject<Book>(myJsonResponse);

    [Table ("Book")]
    public class Book
    {
        [JsonProperty("kind")]
        public string kind { get; set; }

        [JsonProperty("totalItems")]
        public int totalItems { get; set; }

        [JsonProperty("items")]
        [OneToMany]
        public List<Item> items { get; set; }
    } 
    public class AccessInfo
    {
        public string country { get; set; }
        public string viewability { get; set; }
        public bool embeddable { get; set; }
        public bool publicDomain { get; set; }
        public string textToSpeechPermission { get; set; }
        public Epub epub { get; set; }
        public Pdf pdf { get; set; }
        public string webReaderLink { get; set; }
        public string accessViewStatus { get; set; }
        public bool quoteSharingAllowed { get; set; }
    }

    public class Epub
    {
        public bool isAvailable { get; set; }
    }

    public class ImageLinks
    {
        public string smallThumbnail { get; set; }
        public string thumbnail { get; set; }
    }

    public class IndustryIdentifier
    {
        public string type { get; set; }
        public string identifier { get; set; }
    }

    [Table ("Item")]
    public class Item
    {
        public string kind { get; set; }
        public string id { get; set; }
        public string etag { get; set; }
        public string selfLink { get; set; }

        [OneToOne]
        public VolumeInfo volumeInfo { get; set; }
        [OneToOne]
        public SaleInfo saleInfo { get; set; }
        [OneToOne]
        public AccessInfo accessInfo { get; set; }
        [OneToOne]
        public SearchInfo searchInfo { get; set; }
    }

    public class PanelizationSummary
    {
        public bool containsEpubBubbles { get; set; }
        public bool containsImageBubbles { get; set; }
    }

    public class Pdf
    {
        public bool isAvailable { get; set; }
        public string acsTokenLink { get; set; }
    }

    public class ReadingModes
    {
        public bool text { get; set; }
        public bool image { get; set; }
    }


    public class SaleInfo
    {
        public string country { get; set; }
        public string saleability { get; set; }
        public bool isEbook { get; set; }
    }

    public class SearchInfo
    {
        public string textSnippet { get; set; }
    }

    [Table("VolumeInfo")]
    public class VolumeInfo
    {
        [PrimaryKey]
        public string title { get; set; }
        public string subtitle { get; set; }

        [TextBlob("StringsBlobbed")]
        public List<string> authors { get; set; }
        public string publisher { get; set; }
        public string publishedDate { get; set; }
        public string description { get; set; }

        [TextBlob("StringsBlobbed")]
        public List<IndustryIdentifier> industryIdentifiers { get; set; }

        [OneToOne]
        public ReadingModes readingModes { get; set; }
        public int pageCount { get; set; }
        public string printType { get; set; }
        [TextBlob("StringsBlobbed")]
        public List<string> categories { get; set; }
        public double averageRating { get; set; }
        public int ratingsCount { get; set; }
        public string maturityRating { get; set; }
        public bool allowAnonLogging { get; set; }
        public string contentVersion { get; set; }
        [OneToOne]
        public PanelizationSummary panelizationSummary { get; set; }
        [OneToOne]
        public ImageLinks imageLinks { get; set; }
        public string language { get; set; }
        public string previewLink { get; set; }
        public string infoLink { get; set; }
        public string canonicalVolumeLink { get; set; }
    }
}
