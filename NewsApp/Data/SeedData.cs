using Microsoft.EntityFrameworkCore;
using NewsApp.Data;
using NewsApp.Services;

namespace NewsApp.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                if (context == null)
                {
                    throw new ArgumentNullException("Null ApplicationDbContext");
                }

                // Look for any categories.
                if (!context.Categories.Any())
                {
                    Category categoryWorld = new Category
                    {
                        Name = "World"
                    };
                    Category categorySweden = new Category
                    {
                        Name = "Sweden"
                    };
                    Category categoryArt = new Category
                    {
                        Name = "Art"
                    };
                    Category categorySports = new Category
                    {
                        Name = "Sports"
                    };
                    Category categoryHealth = new Category
                    {
                        Name = "Health"
                    };
                    Category categoryBusiness = new Category
                    {
                        Name = "Business"
                    };
                    Category categoryCulture = new Category
                    {
                        Name = "Culture"
                    };

                    // Look for any articles.
                    if (!context.Articles.Any())
                    {
                        context.Articles.AddRange(
                         new Article
                         {
                             Title = "Royal Family greet Queen Elizabeth II's coffin",
                             LinkText = "Royal Family greet Queen Elizabeth II's coffin",
                             Content = "The King was there with Royal Family members including his wife Camilla, the Queen Consort, Princes William and Harry and their wives, Catherine and Meghan.The hearse, accompanied by the Queen's daughter Princess Anne, travelled by road after a flight from Edinburgh.Despite the rain, crowds gathered along the route to pay their respects.There was applause from the thousands gathered outside as the hearse arrived at the palace.Many people lining the street put down their umbrellas as a sign of respect while phone cameras lit up the crowds as people documented the historic moment.The procession to the palace marks one of the final stages of a solemn journey, which began on Sunday at the Queen's Balmoral estate where she died aged 96 on Thursday.Thousands paid their respects as the Queen lay at rest in St Giles' Cathedral, in Edinburgh overnight, before her coffin was flown to RAF Northolt, in north-west London this evening.",
                             ContentSummary = "The Queen's children and grandchildren greeted her coffin on its arrival at Buckingham Palace.",
                             ImageLink = "https://c.files.bbci.co.uk/12E97/production/_126736477_gettyimages-1423631022.jpg",
                             Category = categoryWorld
                         },

                         new Article
                         {
                             Title = "Find out what's going on in Sweden with The Local's roundup",
                             LinkText = "Find out what's going on in Sweden with The Local's roundup",
                             Content = "Sweden Democrats demand no changes to A-kassa job insurance.Sweden Democrat leader Mattias Karlsson is demanding that the Moderates pledge not to slim down  Sweden’s A-kassa job insurance system if they want his party’s backing to form a new government. <br /> First Centre Party district calls for new leader.Tomas Sander, chair of the Centre Party in Borgholm on the island of Öland told the newspaper :“I still have confidence in Annie, but I think that we need to have a new leader before the next election,”. <br /> ‘Shedding voters to Sweden Democrats lost Social Democrats the election’.The decisive shift in voters which looks likely to see Magdalena Andersson ousted as Prime Minister was from the Social Democrats to the Sweden Democrats, the veteran politics professor Sören Holmberg has said.As the Social Democrats are Sweden’s biggest party, that 8 percent represents tens of thousands of voters, helping take the Sweden Democrats to their record result, making them the second biggest party in Sweden.<br />",
                             ContentSummary = "Sweden Democrat unemployment payments demand, talk of new Centre leader, Hédi Fried protest, and why Soc Dems lost the election",
                             ImageLink = "https://idsb.tmgrup.com.tr/ly/uploads/images/2022/09/01/228142.jpg",
                             Category = categorySweden
                         },

                         new Article
                         {
                             Title = "Emmy Awards 2022",
                             LinkText = "Emmy Awards 2022",
                             Content = "On Monday night, it was once again time for some award-winning Ted talks. Success was indeed contained within Succession. And there was a full blossoming of a White Lotus.Apple TV+ comedy Ted Lasso netted several big wins on Monday night at the 74th annual Emmy Awards, scoring four trophies, including its second straight Outstanding Comedy Series and acting awards again for Brett Goldstein and Jason Sudeikis. On the drama side, HBO's Succession had a plan for Emmy excellence: The show claimed trophies for Outstanding Drama Series (its second), Supporting Actor (Matthew Macfayden) and writing (creator Jesse Armstrong). Meanwhile, HBO's The White Lotus had a five-star night in the limited series genre, winning five awards: In addition to the big prize, Jennifer Coolidge and Murray Bartlett scored acting prizes, while creator Mike White triumphed in writing and directing. (The show earned 10 Emmys overall, including last weekend's Creative Arts Emmys.)<br />These are this year’s Emmy winners :<br />Best Comedy : “Ted Lasso” (Apple TV+)<br />Best Drama : “Succession” (HBO)<br />Best Limited Series : “The White Lotus” (HBO)<br />Best Actress, Comedy : Jean Smart, “Hacks”<br />Best Actor, Comedy : Jason Sudeikis, “Ted Lasso”<br />Best Actress, Drama : Zendaya, “Euphoria”<br />Best Actor, Drama : Lee Jung-jae, “Squid Game”<br />",
                             ContentSummary = "See the complete list of winners,Who claimed the most trophies on TV's biggest night? ",
                             ImageLink = "https://i0.wp.com/highoncinemaa.com/wp-content/uploads/2022/09/1663048724_collage.jpg",
                             Category = categoryArt
                         },

                        new Article
                        {
                            Title = "Challenge your friends and test your UEFA Champions League match prediction skills",
                            LinkText = "Challenge your friends and test your UEFA Champions League match prediction skills",
                            Content = "For each match, points are awarded for the correct result, the correct number of home goals, the correct number of away goals and the correct goal difference. You can also score more points by predicting the official Player of the Match and the first team to score during the knockout stages.Bonus points will also be given for predicting a correct scoreline that fewer than 10% of other users plumped for, while you can use a 2x booster to double your potential score for your selected match on each Matchday.The top-ranked player at the end of the campaign will win two tickets to the 2023 UEFA Champions League final (including flights and accommodation).Predictor is completely free to play and you can change your predictions at any point up to the scheduled kick-off time of each match. Take on your friends and try to climb the public leaderboards. Check in anytime ahead of kick-off each matchday and start predicting.",
                            ContentSummary = "Add a different dimension to your enjoyment of the UEFA Champions League by predicting the scorelines of every match with the official Predictor game.",
                            ImageLink = "https://editorial.uefa.com/resources/0278-15ff4d8cf718-e24c7a4e2a65-1000/format/wide1/ucl_20220830172142.jpg",
                            Category = categorySports
                        },

                       new Article
                       {
                           Title = "New malaria vaccine is world-changing, say scientists",
                           LinkText = "New malaria vaccine is world-changing, say scientists",
                           Content = "The team expect it to be rolled out next year after trials showed up to 80% protection against the deadly disease.Crucially, say the scientists, their vaccine is cheap and they already have a deal to manufacture more than 100 million doses a year.The charity Malaria No More said recent progress meant children dying from malaria could end in our lifetimes.It has taken more than a century to develop effective vaccines as the malaria parasite, which is spread by mosquitoes, is spectacularly complex and elusive. It is a constantly moving target, shifting forms inside the body, which make it hard to immunise against.Trial results from 409 children in Nanoro, Burkina Faso, have been published in the Lancet Infectious Diseases. It shows three initial doses followed by a booster a year later gives up to 80% protection.Malaria has been one of the biggest scourges on humanity for millennia and mostly kills babies and infants. The disease still kills more than 400,000 people a year even after dramatic progress with bed nets, insecticides and drugs.",
                           ContentSummary = "A malaria vaccine with world-changing potential has been developed by scientists at the University of Oxford.",
                           ImageLink = "https://th-thumbnailer.cdn-si-edu.com/rtG-9OlkiCC9hfOO7L3SWR-fK0M=/fit-in/1600x0/filters:focal(800x602:801x603)/https://tf-cmsv2-smithsonianmag-media.s3.amazonaws.com/filer_public/6f/5d/6f5d7475-6831-4e03-a0d3-794909be6aa5/gettyimages-1140249297_web.jpg",
                           Category = categoryHealth
                       },

                        new Article
                        {
                            Title = "iPhone 14 and 14 Pro Review: Go Pro or Go Home",
                            LinkText = "iPhone 14 and 14 Pro Review: Go Pro or Go Home",
                            Content = "I hear you. “Wait, wait, wait. In a 12 months when every part prices extra, together with precise apples, you need me to offer $2.5 trillion.Blame the “dynamic island,” a display compromise Apple has became a sensible multitasking trick on the iPhone 14 Pro and Pro Max. Also blame their always-on show and massive cameras.After spending practically per week testing the brand new telephones, I can say the extra “affordable” fashions are good selections, too. But this 12 months—greater than up to now—Apple’s top-of-the-line telephones do extra to justify their $200 worth bump.Plus, a pricier telephone may very well be inside attain now that mobile carriers have gone completely bananas with offers that may knock off lots of. (Yes, I’m conscious banana costs are up, too.) Before I clarify my reasoning, you need to perceive the 2 important iPhone 14 teams:• iPhone 14 ($799 and up) and iPhone 14 Plus ($899 and up): Herein referred to as the “regulars,” these have two cameras and colourful designs. (Gone is the smalr, cheaper Mini possibility.) The 14 has a 6.1-inch display and the Plus has a 6.7-inch display—in any other case, they’re equivalent in design.I plan to assessment the iPhone 14 Plus, due out Oct. 7, at a later date. The different telephones can be found on Sept. 16.",
                            ContentSummary = "Forgive me buddies for what I’m about to say: You ought to simply purchase this 12 months’s $999-and-up iPhone Pros",
                            ImageLink = "https://i.gadgets360cdn.com/large/iphone_14_apple_india_1662629536375.jpg",
                            Category = categoryBusiness
                        },

                        new Article
                        {
                            Title = "Discover Maya history along Mexico’s first thru-hike",
                            LinkText = "Discover Maya history along Mexico’s first thru-hike",
                            Content = "West of Cancún’s tourist-filled beaches, a network of ancient walking paths and disused railway lines has been transformed into the Camino del Mayab (the Maya Way), Mexico’s first long-distance trail.Developed with Maya locals, the trail tells the story of Mexico’s Indigenous peoples and aims to lift the 14 communities that live along its 68-mile route from a history of colonial exploitation and cultural erosion.A three-day bike ride or a five-day hike takes visitors into the heart of the Maya world in Yucatán, from Dzoyaxché, a small community built around the faded yellow walls of a 19th-century hacienda some 15 miles south of Mérida, to the excavated temples of Mayapán, one of the last great Maya capital cities.”After the Spanish conquest of Yucatán in the 16th century, the Maya were left at the bottom of a racial caste system imposed by the European colonizers. The Maya language came second to Spanish, while Maya temples were knocked down and the stones used to build Christian churches.Almost 3,000 years ago, the first Maya cities were carved from forests like the ones in Dzoyaxché, where I join a small group cycling the Camino del Mayab. By the seventh century A.D. Maya civilization had expanded across Central America and southern Mexico, building monumental temples such as those at Chichen Itza in Mexico and Tikal in Guatemala.",
                            ContentSummary = "Three years in the making, the 68-mile hiking and cycling trail visits almost forgotten Maya cultural sites.",
                            ImageLink = "https://i.natgeofe.com/n/ce44d8a8-0a52-436a-8926-145e4792de2a/resized-Hacienda_Yaxcopoil_4x3.jpg",
                            Category = categoryCulture
                        });
                    }
                }
                // Look for any Subscription types.
                if (!context.SubscriptionTypes.Any())
                {
                    context.SubscriptionTypes.AddRange(
                     new SubscriptionType
                     {
                         TypeName = "Basic",
                         Description = "BASIC",
                         Price = 99,
                     },
                     new SubscriptionType
                     {
                         TypeName = "Premium",
                         Description = "PREMIUM",
                         Price = 199,
                     }); 
                }
                else
                {
                    return;   // DB has been seeded        
                }

                context.SaveChanges();
            }
        }
    }
}
