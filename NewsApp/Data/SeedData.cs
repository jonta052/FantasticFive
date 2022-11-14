﻿using Microsoft.EntityFrameworkCore;
using NewsApp.Data;

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
                    Category categoryScience = new Category
                    {
                        Name = "Science"
                    };
                    Category categoryTech = new Category
                    {
                        Name = "Tech"
                    };
                    Category categoryFashion = new Category
                    {
                        Name = "Fashion"
                    };
                    Category categoryTravel = new Category
                    {
                        Name = "Travel"
                    };

                    //context.SaveChanges();
                    // Look for any articles.
                    if (!context.Articles.Any())
                    {
                        context.Articles.AddRange(
                         new Article
                         {
                             Title = "Iran woman protests 2022",
                             LinkText = "Iran woman protests 2022",
                             Content = "Protesters have gathered in cities around the world in recent days in a show of solidarity with women in Iran.The gatherings are an echo of the protests that have erupted in Iran since the death of 22-year-old Mahsa Amini, who had been detained by the country's so-called morality police. Whether in Istanbul or Los Angeles, protests are marked by the striking words and images that have typified the protests rocking Iran: chants of \"Women, life, freedom!\" and in some cases, women taking scissors to their hair.\"This time is different because, in my opinion, is led by the women. Women are the revolutionaries at this time, at this time in history,said protester Aref Alvandi at the D.C. rally, according to ABC News.",
                             ContentSummary = "streets in solidarity with Iranian women",
                             ImageLink = "https://www.idsa.org/sites/default/files/field_main_image/article/Iranian%20protest%20banner%20image%20-%20Copy.jpg",
                             Category = categoryWorld
                         },

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
                             Content = "Sweden Democrats demand no changes to A-kassa job insurance.Sweden Democrat leader Mattias Karlsson is demanding that the Moderates pledge not to slim down  Sweden’s A-kassa job insurance system if they want his party’s backing to form a new government. <br /> First Centre Party district calls for new leader.Tomas Sander, chair of the Centre Party in Borgholm on the island of Öland told the newspaper :“I still have confidence in Annie, but I think that we need to have a new leader before the next election,”. <br /> ‘Shedding voters to Sweden Democrats lost Social Democrats the election’.The decisive shift in voters which looks likely to see Magdalena Andersson ousted as Prime Minister was from the Social Democrats to the Sweden Democrats, the veteran politics professor Sören Holmberg has said.As the Social Democrats are Sweden’s biggest party, that 8 percent represents tens of thousands of voters, helping take the Sweden Democrats to their record result, making them the second biggest party in Sweden.",
                             ContentSummary = "Sweden Democrat unemployment payments demand, talk of new Centre leader, Hédi Fried protest, and why Soc Dems lost the election",
                             ImageLink = "https://idsb.tmgrup.com.tr/ly/uploads/images/2022/09/01/228142.jpg",
                             Category = categorySweden
                         },

                         new Article
                         {
                             Title = "Emmy Awards 2022",
                             LinkText = "Emmy Awards 2022",
                             Content = "On Monday night, it was once again time for some award-winning Ted talks. Success was indeed contained within Succession. And there was a full blossoming of a White Lotus.Apple TV+ comedy Ted Lasso netted several big wins on Monday night at the 74th annual Emmy Awards, scoring four trophies, including its second straight Outstanding Comedy Series and acting awards again for Brett Goldstein and Jason Sudeikis. On the drama side, HBO's Succession had a plan for Emmy excellence: The show claimed trophies for Outstanding Drama Series (its second), Supporting Actor (Matthew Macfayden) and writing (creator Jesse Armstrong). Meanwhile, HBO's The White Lotus had a five-star night in the limited series genre, winning five awards: In addition to the big prize, Jennifer Coolidge and Murray Bartlett scored acting prizes, while creator Mike White triumphed in writing and directing. (The show earned 10 Emmys overall, including last weekend's Creative Arts Emmys.)These are this year’s Emmy winners : Best Comedy :“Ted Lasso” (Apple TV+)_Best Drama : “Succession” (HBO)_Best Limited Series : “The White Lotus” (HBO)_Best Actress, Comedy : Jean Smart, “Hacks”_Best Actor, Comedy : Jason Sudeikis, “Ted Lasso”_Best Actress, Drama : Zendaya, “Euphoria”_Best Actor, Drama : Lee Jung-jae, “Squid Game”",
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
                        },
                        new Article
                        {
                            Title = "Indonesia bans five foreign scientists, shelves conservation data",
                            LinkText = "Researchers say the government tightly controls—and sometimes disputes—population estimates for endangered species",
                            Content = "Even before Dutch conservation scientist Erik Meijaard submitted an opinion piece to The Jakarta Post last month, he was worried about how the Indonesian government would react. In the article, he and four other Western scientists challenged the government’s claims that orangutan populations in the country are thriving. Meijaard was aware that Indonesia is increasingly wary of “foreign interference” in conservation matters and had invited eight Indonesian collaborators to co-author the article. None agreed to do so.",
                            ContentSummary = "Many Indonesian scientists concur, but very few want to talk about it publicly. “Our voices are silenced,” says a conservationist in Sumatra who asked to remain anonymous for fear of reprisals.",
                            ImageLink = "https://www.science.org/do/10.1126/science.adf2372/full/_20221007_nid_indonesia.jpg",
                            Category = categoryScience
                        },
                        new Article
                        {
                            Title = "Evidence suggests pandemic came from nature, not a lab, panel says",
                            LinkText = "New report takes sides in debate over COVID-19’s origins",
                            Content = "The acrimonious debate over the origins of the COVID-19 pandemic flared up again this week with a report from an expert panel concluding that SARS-CoV-2 likely spread naturally in a zoonotic jump from an animal to humans—without help from a lab.\r\n\r\n“Our paper recognizes that there are different possible origins, but the evidence towards zoonosis is overwhelming,” says co-author Danielle Anderson, a virologist at the University of Melbourne. The report, which includes an analysis that found the peer-reviewed literature overwhelmingly supports the zoonotic hypotheses, appeared in the Proceedings of the National Academy of Sciences (PNAS) on 10 October.",
                            ContentSummary = "For nearly 3 years we’ve been running in circles about different lab-leak scenarios, and nothing has really added to this hypothesis,” says co-author Isabella Eckerle, a virologist at the University of Geneva. “We have missed the chance to say … what can we do better the next time?",
                            ImageLink = "https://www.science.org/do/10.1126/science.adf2371/full/_20221007_nid_covidorigins.jpg",
                            Category = categoryScience
                        },
                        new Article
                        {
                            Title = "DroneSeed's Six-Rotor Heavy-Duty Drones Aim to Offer Fast, Affordable Wildfire Reforestation",
                            LinkText = "Using custom - built heavy - lift uncrewed aerial vehicles(UAVs). DroneSeed seeds trees from the air wherever they're needed.",
                            Content = "DroneSeed was founded in 2016 with a mission of making reforestation scalable to mitigate the worst effects of climate change,\" Cassie Meigs, senior director of client solutions at DroneSeed, tells us. \"No off-the-shelf aircraft or deployers would work, so engineers worked for several years to develop a custom-designed, heavy-lift drone platform, software, charging system, and deployers. The first prototype of the seed enablement technology using biomimicry was utilized in 2019. The next two years were spent refining the product and scaling operational capacity",
                            ContentSummary = "Drones are seeing increasing use in industry for everything from site surveys to last-mile delivery services, but DroneSeed is putting its custom craft to work on a different problem: rapidly re-seeding forests after a wildfire, while providing businesses with a way to offset their carbon emissions.",
                            ImageLink = "https://hackster.imgix.net/uploads/attachments/1504100/image_0joupgYKoY.png?auto=compress%2Cformat&w=740&h=555&fit=max",
                            Category = categoryTech
                        },
                        new Article
                        {
                            Title = "The Heated Debate Around Password Security",
                            LinkText = "By incorporating deep learning into thermal attacks, researchers can recover passwords from keyboard heat signatures with greater accuracy.",
                            Content = "Despite all of the headlines about hacks and data breaches that show up far too frequently in our news feeds, even the most basic of security guidelines are regularly disregarded. When people use simplistic, easy to guess passwords to secure their accounts, for example, it makes the work of bad actors much easier. A recent report by Forbes revealed that 59% of Americans use a person’s name or birthday in their password. And these bad passwords are reused an average of 14 times for various different accounts. But if you use a more complex, unguessable password, then you are safe, right?\r\n\r\nNot exactly. Better passwords will certainly help to keep your accounts more secure, but cyber crooks always seem to be one step ahead of the current best security practices. One of the latest tools to emerge in the toolkits of unscrupulous hackers is called a thermal attack. With the cost of capable thermal cameras now below $150, this type of exploit is becoming more common. To pull off this attack, a thermal image of a keyboard, or other input device, is captured within about 30 to 60 seconds after a user has typed in their login credentials. Areas of the input device that have been touched will be warmer than other areas, and the more recently they were touched, the warmer they are. That provides enough information to determine what was pressed and, more or less, in what order.",
                            ContentSummary = "Some practical safety tips also came out of the user studies. It was found, for example, that it was more difficult to recover passwords from touch typists than hunt-and-peck keyboard users, the latter group tending to leave their fingers on keys for longer. Keyboard material also mattered, with ABS plastics retaining heat considerably longer than PBT plastics. Longer passwords do help with security, to be sure, but thanks to tricks like thermal attacks, they may not be enough by themselves.",
                            ImageLink = "https://hackster.imgix.net/uploads/attachments/1507651/media_885927_smxx_XSjGIBaYxJ.jpeg?auto=compress%2Cformat&w=830&h=466.875&fit=min&dpr=1",
                            Category = categoryTech
                        },
                        new Article
                        {
                            Title = "Halle Bailey Is Excited For You to See The Little Mermaid ",
                            LinkText = "When the teaser trailer for the live-action reboot of The Little Mermaid dropped last month, it instantly broke the Internet. The short clip—which now has more than 24 million views and counting—showcases Halle Bailey as Ariel singing a snippet of the classic song, “Part of Your World.” “I was not expecting that reaction! I was so surprised at how viral it all went,” Bailey tells Vogue during Paris Fashion Week.",
                            Content = "The reactions were mostly positive, with several commenters noting that Bailey’s angelic voice sent shivers down my spine. But, as happens for many stars taking on an extremely famous role, there was also backlash, with racist commenters in particular decrying the prospect of a Black Ariel. For her part, Bailey is choosing to focus on the positive—mainly, seeing how her starring role is providing much-needed representation, and how it’s resonating with young Black girls. (There’s been several videos showcasing such girls reacting to the trailer with glee). I saw all of the babies and I would just cry at every single reaction video, says Bailey. I just lost my mind!",
                            ContentSummary = "Until the film’s much-anticipated release in May next year, the star must keep any specific details of the project under wraps. But she did dish on what fans can expect from the film. “People can really expect a new, modern take on the film,” says Bailey. “We all know and love the story, and fell in love with Ariel. This time, it’s about what she wants for herself and her future, instead of it just being all about a boy and falling in love.",
                            ImageLink = "https://assets.vogue.com/photos/6338360dbebbac7c636f41bd/16:9/w_1280,c_limit/Halle_Bailey-Julia_Marino_8.jpg",
                            Category = categoryFashion
                        },
                        new Article
                        {
                            Title = "Katie Holmes Proves Eco-Conscious Trainers Don’t Have to Be Boring",
                            LinkText = "Katie Holmes is known for her effortless, girl-next-door style – but she’s not afraid to add an attention-grabbing accessory into the mix. Case in point: Chloé’s eco-conscious Nama sneakers, which she wore while out in New York with boyfriend Bobby Wooten III.",
                            Content = "Katie Holmes is known for her effortless, girl-next-door style – but she’s not afraid to add an attention-grabbing accessory into the mix. Case in point: Chloé’s eco-conscious Nama sneakers, which she wore while out in New York with boyfriend Bobby Wooten III.\r\n\r\nThe colorful trainers, which are made from recycled mesh and Leather Working Group-certified suede, are the latest addition to Holmes’s sustainable footwear collection. In fact, the actor also owns the style in white and beige, and wore the brand’s Joy clogs several times over the summer.\r\n\r\nFor a self-confessed “jeans girl at heart”, Holmes also decided to switch it up on the trouser front, opting for a pair of beige cargo pants, which she teamed with a denim shirt and tote bag slung over her shoulder. \r\n\r\nWhen attending the Chloé show in Paris last week, Holmes explained her approach to sustainable fashion: “My personal style is pretty classic… I tend to have a few good pieces and re-wear them.” It’s an ethos we would all do well to live by.",
                            ContentSummary = "When attending the Chloé show in Paris last week, Holmes explained her approach to sustainable fashion: “My personal style is pretty classic… I tend to have a few good pieces and re-wear them.” It’s an ethos we would all do well to live by.",
                            ImageLink = "https://media.vanityfair.com/photos/6290d0f54bf214a6e50bfed4/master/w_2560%2Cc_limit/1399473229",
                            Category = categoryFashion
                        },
                        new Article
                        {
                            Title = "25th Tartu Mountain Bike Marathon",
                            LinkText = "Throughout Estonia",
                            Content = "Tartu Mountain Bike Marathon invites all sports fans of different ages and level of training. The main distances of 89, 40, and 21 km take participants away from the hustle and bustle of the city, to the varied landscape trails of Southern Estonia. The track largely runs along the famous Tartu Marathon route. The 89 and 40 km tracks start from Otepää while the 21 km track starts from Palu. The finish of all three tracks is located between the beautiful pine forests of Elva. The day before the main event, there will be events for the youngest members of the family. Those who wish can take part in the marathon virtually on their home track. More information and registration on the website.",
                            ContentSummary = "Those who wish can take part in the marathon virtually on their home track. More information and registration on the website.",
                            ImageLink = "https://static.visitestonia.com/images/3596292/1000_500_false_false_7988aedc9454a114bfc1b12224bfb3be.jpg",
                            Category = categoryTravel
                        },
                        new Article
                        {
                            Title = "A place where counterculture meets high-tech",
                            LinkText = "San Francisco is a place of landmarks and photo opportunities, not the least of which is the Golden Gate Bridge. Crossing the famous suspension bridge that spans San Francisco Bay via bicycle, by car or on foot is a must.",
                            Content = "San Francisco is the kind of quirky place where a fog-calling contest might compete for attention with gala balls marking the opening of the symphony, opera and ballet seasons. From Union Square to North Beach to Japantown, you’ll find intriguing neighborhoods at every turn. The writers of the Beat Generation, the hippies of the Summer of Love in the late 1960s and the gay and lesbian population contributed in making the city what it is today. San Francisco is also one of the world’s great dining destinations, thanks in part to the diverse cultural influences and proximity of fresh ingredients.\r\n\r\nIt’s a city of contrasts – old and new, cutting-edge and laid-back, urban and pastoral. This is a city in constant evolution. Add iconic sights like the Golden Gate Bridge, Fisherman’s Wharf, Alcatraz Island and intriguing neighborhoods as individualistic as they are dynamic, and San Francisco becomes captivating, creative and truly unique.",
                            ContentSummary = "Dim sum is everywhere in Chinatown, and North Beach is famous for its Italian food. In the shopping paradise of Union Square, find Michelin-starred and celebrity chef restaurants alongside old-school establishments and casual eateries. ",
                            ImageLink = "https://www.visittheusa.se/sites/default/files/styles/hero_xl_1600x700/public/images/hero_media_image/2016-10/Getty_510067479_Brand_City_SanFrancisco_Rotator_FinalCrop.jpg?itok=FvQ4Q671",
                            Category = categoryTravel
                        },
                        new Article
                        {
                            Title = "UK announces sanctions against Iran’s morality police",
                            LinkText = "Move comes in response to violent suppression of protests over death of Mahsa Amini in police custody",
                            Content = "Britain has announced sanctions against Iran’s morality police in its entirety as well as its national chief and the head of its Tehran division in response to the violent suppression of protests since the death of 22-year-old Mahsa Amini in police custody.\r\n\r\nThe morality police have been responsible for the street patrols forcing women to wear hijab and attend re-education classes on modesty and chastity. Amini was stopped by the morality police over her clothing while walking in a park in Tehran and taken into detention.\r\n\r\nSimilar sanctions have already been imposed by the US and are set to be imposed by the EU.",
                            ContentSummary = "The sanctions are intended to ensure that the individuals listed cannot travel to the UK, and that any of their assets held in the UK, or by UK persons anywhere, will be frozen.",
                            ImageLink = "https://i.guim.co.uk/img/media/c016075476e83e4f9ad1df18ec5016c020a374ef/0_117_3500_2100/master/3500.jpg?width=620&quality=85&dpr=1&s=none",
                            Category = categoryWorld
                        },
                        new Article
                        {
                            Title = "Sweden election: Why the far-right were the biggest winners and four other takeaways",
                            LinkText = " Sweden Democrats supporters celebrate a strong showing on election night, Sunday 11 September 2022, at a hotel near Stockholm",
                            Content = "When all the votes were counted the Sweden Democrats might not have been the biggest party in this week's election, but there's no doubt they were the biggest winners. \r\n\r\nFrom early beginnings as a neo-Nazi group to getting their first MPs into the Riksdag only 12 years ago; to supplanting the traditional conservative opposition and garnering more than 20% of the vote this year, the rise of the far-right anti-immigrant party has been meteoric. \r\n\r\nThe Sweden Democrats may not end up a formal part of the right-wing coalition which has a slim majority in the Swedish parliament, but they will certainly loom large behind the scenes, in the corridors of power, and where government ministries are driven by some of their policies.",
                            ContentSummary = "The Sweden Democrats did not have a flash-in-the-pan win at this week's election. They've had a slow and steady build-up of support over the last decade and seen the success of similar anti-immigrant parties in Finland and Denmark getting into government, paving the way for them.",
                            ImageLink = "https://static.euronews.com/articles/stories/07/02/04/68/773x435_cmsv2_f1c6989f-0a39-5aa9-afbe-1c90edc1534b-7020468.jpg",
                            Category = categorySweden
                        },
                        new Article
                        {
                            Title = "‘More serious than the pandemic’: UK museums will struggle to keep doors open without government intervention, top director warns",
                            LinkText = "Director-general of National Galleries of Scotland warns the country's most historic art institutions will face partial closure without support, as energy bills are forecast to double next year ",
                            Content = "One of the most senior voices in the UK museum sector has warned the UK’s most established cultural institutions will “struggle to keep their doors open” if local and central authorities fail to intervene.\r\n\r\nJohn Leighton has worked as the director-general of the National Galleries of Scotland (NGS) since 2006. “I have never experienced a crisis like this in my career,” he says. “It’s more serious than the pandemic.”\r\n\r\nLeighton’s message will likely reverberate through the UK’s museum community, as well as a Scottish public faced with the prospect of highly reduced access to the country’s proud artistic and cultural heritage.",
                            ContentSummary = "More serious than the pandemic’: UK museums will struggle to keep doors open",
                            ImageLink = "https://cdn.sanity.io/images/cxgd3urn/production/b70e2866b40d4d715806fdeb2a43e55a4d2433ff-3287x2266.jpg?rect=0,0,3286,2266&w=1920&h=1324&fit=crop&auto=format",
                            Category = categoryArt
                        },
                        new Article
                        {
                            Title = "The Biggest Issues Within Today’s Sports",
                            LinkText = "Whether it’s the NBA, MLB, NFL, or even the NHL, there will always be flaws within sports. While some sports share multiple issues, some of the problems presented have to do with particular aspects of the game. A lot of issues don’t contain immediate solutions, while some only require a simple rule change by the commissioner. Here are some of the biggest issues within today’s sports:",
                            Content = "he most recent NBA example of a “super team” happens to involve the 2017 Golden State Warriors, but this issue has been around the league for ages. The ’96 Bulls, 2008 Celtics, and 2011 Heat are just a few more examples of superstar-ran franchises that have dominated their respected conferences, and the numbers show they were all virtually unstoppable during the regular season. By combining the regular season record of all four of the aforementioned franchises, during their championship runs, they totaled a regular season record of 251-61 (.804 winning percentage).\r\n\r\nSure, the Bulls had the Jazz, the Celtics rivaled the Lakers, the Heat fought the Celtics, and the Warriors have had the Cavs in the postseason, but the NBA has become somewhat predictable through the regular season.  The players on top of the world prefer to play for a championship, and while that should always be their goal, a lot of players no longer hold a value of loyalty to one home.\r\n\r\nThe biggest issue with super teams in the NBA is that there is no one way to stop it from occurring.  Do you limit franchises to maxing the contract of one player? Do you lower the overall salary cap for all teams? There is not an answer that everyone can agree on, because lets face it, if we knew the solution, it would no longer be an issue for critics such as myself.",
                            ContentSummary = "A lot of this reasoning has to do with how much money the NHL brings in terms of revenue, which is less than most of the other professional leagues. While they don’t bring in nearly as much revenue, this is still a $3.7 billion industry. The stars of the NHL deserve more than what they are being given, as the NHL is one of the most talked about sports in some parts of the world.",
                            ImageLink = "https://i0.wp.com/theathleteshub.org/wp-content/uploads/2017/06/img_1885.jpg?resize=780%2C405&ssl=1",
                            Category = categorySports
                        },
                        new Article
                        {
                            Title = "How Many Is Too Many? - When Drinking Becomes a Problem ",
                            LinkText = "For some people, a glass of wine, a beer, or a cocktail is an occasional treat. Others struggle to stop at just one or even many drinks. Some may drink alcohol in moderation, but still feel like they’re not in control of their drinking. How do you know if alcohol has become a problem for you?",
                            Content = "Some people should avoid alcohol completely. These include pregnant women and people who take certain medications. For most adults, experts recommend men limit alcohol to no more than two drinks per day, and women to no more than one drink per day. Drinking less is better for your health than drinking more.And you aren’t supposed to save those up and drink them all on a Saturday night, says Dr. George Koob, director of NIH’s National Institute on Alcohol Abuse and Alcoholism. This type of drinking, called binge drinking, is especially dangerous. Binge drinking is when you have five or more drinks within a few hours for men, and four or more for women. Examples of a standard drink size are a 12-ounce glass of beer, a 5-ounce glass of wine, or 1.5 ounces of hard liquor.Binge drinking can lead to blackouts or even deadly overdoses. Heavy drinking in general can cause many health problems. These include liver disease, heart and lung problems, and muscle and bone weakness.There has been some recent good news about drinking in the U.S. We’ve seen a steady decline in underage drinking, Koob says. And deaths associated with driving while intoxicated have, overall, gone down.We’ve also seen movements for things like ‘dry January,’ ‘sober curious,’ and bars serving non-alcoholic cocktails, Koob explains. People are more aware that there are individuals who don’t want to drink.But there’s also bad news. Over the last two decades, deaths involving alcohol use have more than doubled. The biggest increase has been for women.The COVID-19 pandemic may have made things worse. Quite a few studies indicate that people tried to cope with the stress of the pandemic by drinking, says Koob. And there were stressors all over the place. Isolation. Loss of jobs. Worry about getting sick. And, for women in particular, increases in responsibilities at home.when Drinking",
                            ContentSummary = "“Seek help, whether it’s from your health care provider, or a pastor, or a friend,” says Diazgranados. “There’s always someone willing to help you through treatment.",
                            ImageLink = "https://i0.wp.com/post.healthline.com/wp-content/uploads/2019/08/friends-bar-drinking-table-still-life-wine-beer-1296x728-header-1296x728.jpg?w=1155&h=1528",
                            Category = categoryHealth
                        },
                        new Article
                        {
                            Title = "Should the 100 - year - old Hughes House be named a historic landmark ? A city panel says yes",
                            LinkText = "Nearly a year since its owners made plans to raze the home, the structure could soon enter its third act as an upscale wine and cheese bar.",
                            Content = "With a city panel on Wednesday in agreement that the Hughes House in Tobin Hill is worthy of historic designation, a century-old San Antonio home could be free from future threats of demolition. \r\n\r\nNow nearly a year since its owners made plans to raze the Prairie-style home, halted by pleas from neighbors and the Conservation Society of San Antonio, the structure could soon enter its third act as an upscale neighborhood wine and cheese bar. \r\n\r\nThe Historic and Design Review Commission voted unanimously to approve the neighborhood’s request for a finding of historic significance, making the Hughes House eligible for landmark designation — but only if the owner agrees or City Council passes a resolution. ",
                            ContentSummary = "It really does typify an era in San Antonio that’s very significant and it would be a huge loss if it ever did come up for demolition again,” he said.",
                            ImageLink = "https://i0.wp.com/sanantonioreport.org/wp-content/uploads/2021/11/nickwagner-development-house-historic-property-tobin-hill-16NOV21-2.jpg?resize=1200%2C868&ssl=1",
                            Category = categoryBusiness
                        },
                        new Article
                        {
                            Title = "Madonna baffles the internet with 'I'm Gay' TikTok video",
                            LinkText = "The “Material Girl” singer, a staunch ally of the LGBTQ community, has publicly been in long-term relationships only with men.",
                            Content = "A viral video of gay icon Madonna appearing to suggest that she is gay herself has taken the internet by storm. \r\n\r\nIn a video posted on the singer’s TikTok account Sunday, she holds up a pair of pink underwear next to the caption, “If I Miss, I’m Gay!” She tries to chuck the underwear in a trash can — and misses — before the video cuts to her dramatically turning away from the camera. \r\n\r\nAfter racking up millions of views, Madonna became the No. 1 most searched topic on Google in the United States at one point Monday. Social media buzzed with reactions. \r\n\r\n“Did I just witness Madonna coming out, good for her,” one of the top comments on the video says, followed by, “Madonna has been an out bisexual for literally decades my dudes” and “Her PR team is freaking out.” ",
                            ContentSummary = "Representatives for Madonna did not immediately respond to NBC News’ request for comment.",
                            ImageLink = "https://media-cldnry.s-nbcnews.com/image/upload/t_fit-1240w,f_auto,q_auto:best/rockcms/2022-10/221010-madonna-al-1319-2015d1.jpg",
                            Category = categoryCulture

                        });

                    }
                    context.SaveChanges();
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
                    context.SaveChanges();
                }
                else
                {
                    return;   // DB has been seeded        
                }


            }
        }
    }
}
