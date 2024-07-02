using BenchmarkDotNet.Attributes;

namespace WordFinder.Benchmark;

[MemoryDiagnoser(false)]
public class WordFinderBenchmark
{
    #region test data declaration
    string[] matrix1 = { "abcgc", "fgwio", "chill", "pqnsd", "uvdxy" };
    string[] words1 = { "chill", "cold", "snow", "wind" };

    string[] matrix2 =
    {
        "lightorangejumpballtreefootnightcatdogsunstarhillcarwinwaterfire",
        "doorbridgebirdhousecakeboatgreenleabluefishmountainskycloudriver",
        "rainrockmoonplanetcloudhorsesnowshipwiandwavebeachearthflameleaf",
        "mountainflifghtbirdtigerplantdreambarkshadowfencelakeshadowlight",
        "riversongfireafliesmoudntaindsnowshdipcldoudleafcloudbridgebeach",
        "cakeshadowbirdhddillbddddluecarfaaaootwavewinddreamaapplesnowdog",
        "watershipfencdelakedjumpplantbdarkfootmountainhillwdaveearthfire",
        "mountainapplewindbirdmooncloudgreenleafflightdogbeacshcarsdhadow",
        "blueearthleafbirdriveqrsnowjumwpmountainfirewatrerrockbreezefire",
        "shadewavecarfishflightbridgeehillrttreeplanetlirghtcloudsongtree",
        "hillmountainwaterwavejumepbreezelightrearthsrhadowbirdriverflame",
        "dreamjumpbarkwaveappflelakrefligehttigershardestnowbirrdleaffoot",
        "lightfireplranertcarerrainxbrridgebreezewaterwavebirdprlantshade"
    };

    string[] words2 =
    {
        "chill", "cold", "snow", "wind", "frost", "blizzard", "ice", "sleet",
        "hail", "glacier", "storm", "freeze", "nippy", "icy", "wintry",
        "shiver", "polar", "arctic", "flurry", "gale", "tundra", "crisp",
        "frozen", "subzero", "brisk", "draft", "breeze", "cool", "numb",
        "slush", "bitter", "whirlwind", "dew", "drizzle", "mist", "fog",
        "overcast", "cloudy", "gloomy", "damp", "humid", "muggy", "rain",
        "showers", "downpour", "deluge", "sprinkle", "puddle", "thunder",
        "lightning", "stormy", "tempest", "cyclone", "hurricane", "typhoon",
        "monsoon", "gust", "squall", "breeze", "zephyr", "calm", "tranquil",
        "still", "balmy", "mild", "temperate", "warm", "hot", "swelter",
        "scorch", "blister", "sizzle", "sear", "torrid", "heatwave",
        "sunny", "clear", "bright", "glow", "shine", "radiant", "dazzle",
        "beam", "ray", "solstice", "equinox", "climate", "weather",
        "forecast", "meteorology", "temperature", "atmosphere", "pressure",
        "humidity", "precipitation", "evaporation", "condensation", "barometer",
        "thermometer", "anemometer", "rainfall", "cloud", "stratus", "cumulus",
        "nimbus", "cirrus"
    };

    string[] words3 =
    {
        "apple", "orange", "banana", "grape", "kiwi", "lemon", "lime", "mango",
        "peach", "pear", "plum", "berry", "melon", "cherry", "fig", "date",
        "quince", "papaya", "avocado", "nectarine", "apricot", "pomegranate", "persimmon",
        "starfruit", "dragonfruit", "lychee", "tangerine", "clementine", "grapefruit",
        "raspberry", "blueberry", "blackberry", "strawberry", "currant", "cranberry",
        "gooseberry", "elderberry", "boysenberry", "mulberry", "huckleberry", "pistachio",
        "cashew", "almond", "walnut", "pecan", "hazelnut", "macadamia", "pineapple",
        "jackfruit", "guava", "passionfruit", "soursop", "rambutan", "durian", "salak",
        "longan", "pomelo", "tamarind", "mangosteen", "yuzu", "custardapple", "satsuma",
        "mandarin", "carrot", "potato", "tomato", "onion", "garlic", "ginger", "beet",
        "radish", "turnip", "parsnip", "rutabaga", "pumpkin", "squash", "zucchini",
        "cucumber", "lettuce", "spinach", "kale", "collard", "arugula", "chard",
        "escarole", "endive", "frisee", "bokchoy", "cabbage", "broccoli", "cauliflower",
        "brusselsprout", "asparagus", "celery", "fennel", "leek", "shallot", "scallion",
        "chive", "parsley", "cilantro", "basil", "mint", "oregano", "thyme", "rosemary",
        "sage", "dill", "tarragon", "marjoram", "savory", "bay", "catnip", "lemonbalm",
        "lavender", "chamomile", "hops", "saffron", "caraway", "coriander", "cumin",
        "fennelseed", "mustardseed", "poppyseed", "sesameseed", "sunflowerseed", "cardamom",
        "cinnamon", "clove", "nutmeg", "mace", "allspice", "juniper", "pepper",
        "paprika", "turmeric", "chili", "cayenne", "wasabi", "horseradish", "gingerroot",
        "galangal", "tamarind", "miso", "soy", "tofu", "tempeh", "seitan", "soba",
        "udon", "ramen", "vermicelli", "spaghetti", "macaroni", "penne", "fettuccine",
        "linguine", "tagliatelle", "pappardelle", "ravioli", "tortellini", "gnocchi",
        "lasagna", "manicotti", "cannelloni", "ziti", "rigatoni", "farfalle", "fusilli",
        "rotini", "orzo", "quinoa", "couscous", "bulgur", "farro", "barley",
        "oats", "millet", "sorghum", "teff", "wheat", "rye", "spelt", "kamut",
        "amaranth", "buckwheat", "corn", "maize", "rice", "basmati", "jasmine",
        "sushi", "arborio", "carnaroli", "glutinous", "wildrice", "bread", "baguette",
        "ciabatta", "focaccia", "pita", "naan", "tortilla", "chapati", "paratha",
        "roti", "bagel", "brioche", "croissant", "scone", "muffin", "biscuit",
        "crumpet", "pancake", "waffle", "crepe", "blini", "latke", "beignet",
        "churro", "doughnut", "pretzel", "sourdough", "ryebread", "wholewheat",
        "multigrain", "whitebread", "brownrice", "parboiled", "sticky", "shortgrain",
        "longgrain", "instant", "canned", "fresh", "frozen", "dried", "preserved",
        "pickled", "fermented", "smoked", "salted", "cured", "marinated",
        "grilled", "roasted", "baked", "fried", "steamed", "poached", "broiled",
        "boiled", "blanched", "braised", "simmered", "stewed", "barbecued",
        "charbroiled", "caramelized", "candied", "glazed", "coated", "stuffed",
        "filled", "frosted", "iced", "creamed", "sugared", "sprinkled",
        "garnished", "topped", "seasoned", "flavored", "spiced", "herbed",
        "aromatic", "fragrant", "savory", "umami", "salty", "sweet", "bitter",
        "sour", "tangy", "tart", "acidic", "pungent", "zesty", "piquant",
        "mild", "spicy", "hot", "fiery", "bland", "delicate", "rich",
        "creamy", "buttery", "crispy", "crunchy", "chewy", "gooey", "sticky",
        "juicy", "moist", "dry", "tender", "tough", "rubbery", "gristly",
        "fibrous", "flaky", "meaty", "pulpy", "fleshy", "leafy", "woody",
        "herbaceous", "fruity", "nutty", "earthy", "mushroomy", "peppery",
        "citrusy", "zesty", "spicy", "savory", "bitter", "sour", "umami",
        "toasty", "smoky", "roasted", "caramelized", "burnt", "charred",
        "grilled", "barbecued", "fried", "stirfried", "deepfried", "sauteed",
        "seared", "poached", "boiled", "simmered", "braised", "stewed",
        "baked", "roasted", "steamed", "microwaved", "cooked", "uncooked",
        "raw", "fresh", "frozen", "canned", "preserved", "dried",
        "pickled", "fermented", "salted", "smoked", "cured", "marinated",
        "coated", "glazed", "candied", "frosted", "iced", "stuffed",
        "filled", "topped", "seasoned", "flavored", "spiced", "herbed",
        "diced", "chopped", "minced", "sliced", "shredded", "grated",
        "peeled", "pitted", "seeded", "crushed", "mashed", "pureed",
        "blended", "whipped", "beaten", "stirred", "mixed", "kneaded",
        "rolled", "shaped", "molded", "formed", "pressed", "poured",
        "drizzled", "sprinkled", "dusted", "dolloped", "spread", "spooned",
        "ladled", "measured", "weighed", "sieved", "strained", "filtered",
        "rinsed", "washed", "cleaned", "scrubbed", "peeled", "trimmed",
        "tied", "skewered", "wrapped", "foiled", "parchment", "paper",
        "plastic", "glass", "metal", "wood", "bamboo", "ceramic",
        "stoneware", "porcelain", "earthenware", "enamel", "castiron",
        "nonstick", "stainless", "aluminum", "copper", "bronze", "brass",
        "silver", "gold", "platinum", "titanium", "nickel", "zinc",
        "lead", "iron", "steel", "chrome", "tungsten", "mercury",
        "bismuth", "antimony", "arsenic", "cadmium", "selenium", "tellurium",
        "polonium", "astatine", "radon", "francium", "radium", "actinium",
        "thorium", "protactinium", "uranium", "neptunium", "plutonium", "americium",
        "curium", "berkelium", "californium", "einsteinium", "fermium", "mendelevium",
        "nobelium", "lawrencium", "rutherfordium", "dubnium", "seaborgium", "bohrium",
        "hassium", "meitnerium", "darmstadtium", "roentgenium", "copernicium", "nihonium",
        "flerovium", "moscovium", "livermorium", "tennessine"
    };
    #endregion
    
    [Benchmark]
    public void TestScenario1()
    {
        new WordFinder(matrix1).Find(words1);
    }
    
    [Benchmark]
    public void TestScenario2()
    {
        new WordFinder(matrix2).Find(words2);
    }
    
    [Benchmark]
    public void TestScenario3()
    {
        new WordFinder(matrix2).Find(words3);
    }
    
    [Benchmark]
    public void TestScenario4()
    {
        new WordFinder(matrix1).Find(words3);
    }
}