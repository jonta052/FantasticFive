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
                if (context == null || context.Articles == null)
                {
                    throw new ArgumentNullException("Null ApplicationDbContext");
                }

                // Look for any categories.
                if (context.Categories.Any())
                {
                    return;   // DB has been seeded
                }
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
                //context.Categories.AddRange(
                //    categoryLocal,
                //    categoryWorld

                //);

                // Look for any articles.
                if (context.Articles.Any())
                {
                    return;   // DB has been seeded
                }

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
                        
                        Title = "World Article",
                        LinkText = "",
                        Content = "",
                        ContentSummary = "Romantic Comedy",
                        ImageLink = "http....",
                        Category = categoryLocal
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
