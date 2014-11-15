namespace MotivateMe.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using MotivateMe.Data.Common;
    using MotivateMe.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        private Random rnd;
        private UserManager<ApplicationUser> userManager;

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            
            //TODO: Make false for Production
            this.AutomaticMigrationDataLossAllowed = true;

            this.rnd = new Random();
        }

        protected override void Seed(ApplicationDbContext context)
        {
            if (context.Articles.Any() || context.Campaigns.Any() || context.Stories.Any() || context.Users.Any())
            {
                return;
            }
            var usernamesUnique = new HashSet<string>();

            this.userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            this.SeedRoles(context);

            for (int i = 0; usernamesUnique.Count <= 20; i++)
            {
                usernamesUnique.Add(GetRandomName());
            }

           

            var users = new List<ApplicationUser>();
            var usernamesUniqueList = usernamesUnique.ToList();
            this.SeedUsers(context, usernamesUniqueList);

            //for (int i = 0; i < usernamesUnique.Count; i++)
            //{

            //    users.Add(new ApplicationUser() { Email = usernamesUniqueList[i] + "@pesho.com", UserName = usernamesUniqueList[i] });
            //} 
            
            context.SaveChanges();

            // Create collections 
            var stories = new List<Story>();
            var articles = new List<Article>();
            var campaigns = new List<Campaign>();
            var tips = new List<Tip>();
            var dbUsers = context.Users.ToList();

            for (int i = 0; i < 20; i++)
            {
                var author = dbUsers[rnd.Next(0, users.Count)];
                var authorId = context.Users.Where(u => u.UserName == author.UserName).Select(user => user.Id).FirstOrDefault();
                stories.Add(new Story() { Author = author, AuthorId = authorId, CreatedOn = GetRandomDate(), Title = GetRandomTitle(), StoryContent = new StoryContent() { After = GetRandomContent(), Before = GetRandomContent(), Conclusion = GetRandomContent(), Experience = GetRandomParagraph() }, IsDeleted = false });
                articles.Add(new Article() { Author = author, AuthorId = authorId, CreatedOn = GetRandomDate(), Title = GetRandomTitle(), Content = GetRandomParagraph(), IsDeleted = false });
                campaigns.Add(new Campaign() { Author = author, AuthorId = authorId, CreatedOn = GetRandomDate(), Title = GetRandomTitle(), Goal = GetRandomTitle(), Info = GetRandomParagraph(), IsDeleted = false });
                tips.Add(new Tip() { Author = author, AuthorId = authorId, CreatedOn = GetRandomDate(), Content = GetRandomContent(), IsDeleted = false });
            }

            foreach (var story in stories)
            {
                context.Stories.Add(story);
            }
            context.SaveChanges();

            foreach (var article in articles)
            {
                context.Articles.Add(article);
            }
            context.SaveChanges();

            foreach (var tip in tips)
            {
                context.Tips.Add(tip);
            }
            context.SaveChanges();

            foreach (var campaign in campaigns)
            {
                context.Campaigns.Add(campaign);
            }
            context.SaveChanges();

            //GetRandomDate();
            //GetRandomUser();
            //GetRandomParagraph();
            //GetRandomTitle();
            //GetRandomContent();
            //GetRandomName();
        }

        private void SeedRoles(ApplicationDbContext context)
        {
            context.Roles.AddOrUpdate(x => x.Name, new IdentityRole(GlobalConstants.AdministratorRoleName));
            context.SaveChanges();
        }

        private void SeedUsers(ApplicationDbContext context, IList<string> uniqueUsernames)
        {
            if (context.Users.Any())
            {
                return;
            }

            for (int i = 0; i < uniqueUsernames.Count; i++)
            {
                var username = uniqueUsernames[i];
                var user = new ApplicationUser
                {
                    Email = string.Format("{0}@{1}.com", username, "mysite"),
                    UserName = username
                };

                this.userManager.Create(user, "123456");
            }

            var adminUser = new ApplicationUser
            {
                Email = "admin@mysite.com",
                UserName = "Administrator"
            };

            this.userManager.Create(adminUser, "admin123456");

            this.userManager.AddToRole(adminUser.Id, GlobalConstants.AdministratorRoleName);
        }
        private DateTime GetRandomDate()
        {
            DateTime start = new DateTime(2012, 1, 1);

            int range = (DateTime.Today - start).Days;
            return start.AddDays(rnd.Next(range));
        }

        private string GetRandomName()
        {
            var names = new List<string>() {
                "LizbethLuckett",  
                "IlseIshikawa",
                "JoanJarmon",  
                "LenLaunius",  
                "SophiaShannon",  
                "FreemanFasching",  
                "TiffanieTrivedi",  
                "MaribethMedlen",  
                "BelleBoughton",  
                "AnetteAntos",  
                "ShakitaSlaybaugh",  
                "TessaTschanz",  
                "CasseyCasavant",  
                "ThelmaTiedemann",  
                "VidaVogt",  
                "DeliaDenbow",  
                "JunitaJanis",  
                "KathrynKrein",  
                "DarcelDilullo",  
                "AltheaAlpers",  
                "JulioJudge",  
                "LeanneLongshore",  
                "TeresaTubbs",  
                "DonetteDewalt",  
                "TaiTalkington",  
                "YunYurick",  
                "AshlynAiken",  
                "ElayneEarley",  
                "ChingCagle",  
                "VincenzaVoegele",  
                "StephanySwartout",  
                "ShemekaSenecal",
                "TammyTowles",  
                "MerissaMunn",  
                "YokoYoungblood",  
                "BellBoulton",  
                "EstaEllenberger",  
                "RandiRousseau",  
                "MildaMorelock",  
                "CristobalCurran",  
                "BrandaBohanon",  
                "VeronaVanover",  
                "AletaAmavisca",  
                "CoryCalderone",  
                "CorrineClauss",  
                "KaiKraushaar",  
                "RandyRayfield",  
                "SharieStronach",  
                "ChiaCaughey",  
                "VivienneVrabel",  
            };
            return names[rnd.Next(0, names.Count)];
        }

        private ApplicationUser GetRandomUser()
        {
            var username = GetRandomName();
            var user = new ApplicationUser() { Email = username + "@pesho.com", UserName = username };
            return user;
        }

        private string GetRandomContent()
        {
            var contents = new List<string>()
            { 
                "Tentation simplificate del e, nos nomine independente tu, de nos altere denominator initialmente? E nomine philologos sed, o duo texto dictionario concretisation, union peano traduction su non. Sed libro libere ascoltar le, web da celos movimento. Sed il sine romanic immediatemente, es pro terra westeuropee. Del veni europee apprende o, se pro instruite paternoster.",
                "Per il lista major americano, de international representantes del! Titulo publication immediatemente duo in, ma articulos finalmente principalmente per, titulo internet anglo-romanic lo web! Del europee angloromanic primarimente le. Via se tres ample instruite, sia al signo auxiliar responder, nos le major origine secundo. Complete involvite tu que, qui peano original su. Nomine synonymo linguistas sia le.",
                "Del un publication independente, rapide technic in pro, con germano interlingua tu. Sia regno resultato de, de pro summario instruite, duo da celos preparation. Parlar concretisation unidirectional uno o, human introductori uso ha. Da signo linguas qualcunque pan! Sia ha inter vices libere, como secundo es qui? Quales questiones technologia tu pro!",
                "Per appellate computator de. Tu sine post technic non, mundial scientia lo pro? Tu usate disuso representantes via? Uso facto technologia denominator tu, pro spatios philologos o! Que un deler original programma, celo litteratura essentialmente da non?",
                "Cinque promotores web il. Se synonymo denominator sia, toto malo historia tu duo, uno es post inviar instruction? Duo usos demonstrate methodicamente le, da qui etiam concretisation. Pro durante professional da, que infra regula tentation il. Libere preparation qui o. Romanic publicava sanctificate ma per. Duo da sitos scientia technologia!",
                "Utilitate scientific linguistas pro su, se qui technologia simplificate interlinguistica. Pan o existe brevissime, per giuseppe interlingua in. Uno de cadeva periodicos unidirectional, linguas programma linguistas es sed, linguage connectiones professional non tu. Medio historiettas del e, technologia initialmente lo con, pro su altere pardona synonymo!",
                "Romanic parolas conferentias su non, lo duo traduction promotores, debitas intermediari ha que! Sed pardona responder lo. Un del language denominator! Duo da flexione language. Con programma summarios ha, duo da populos tentation.",
                "Un via libera anteriormente, secundo linguage grammatica pro ma, ma que esser gymnasios connectiones! Sed se super europeo millennios, qui ha articulo hereditage. Libro millennios secundarimente in sed, lo sed como latino denomination. Pro lo complete incorporate! Su del unic infra inter? Tres signo il duo, sed ma americas intention representantes.",
                "Qui le multo personas comprende, es nos contos flexione publicationes? Pardona publicationes non ha, se web titulo language sanctificate, sed e anque anglese? Es del regula periodicos, sed debe sitos un. Pan major human maximo un, duce interlinguistica via e.",
                "Sine post da con, membros apprende giuseppe da sed. Su pro usate encyclopedia, summario responder philologos uso tu! Es africa independente interlinguistica web, parolas specimen non al! Nos un rapide tamben, sed clave contos linguistas in.",
                "Su uso populos auxiliary, nostre language pro un? Basate programma duo tu, con vide mundial de, ha del debe union? Russo vostre via ha, ille europee original il que, libera parolas introductori al duo. Al rapide europeo denominator pan! Message articulos sed tu, unic original lo del. Lo via como disuso flexione.",
                "Spatios apprende programma uno il, da sed peano articulos summarios! Un message laborava via, le giuseppe scientific pro, via spatios linguage demonstrate ma. Da conferentias introductori que. Non texto historiettas in, ha patre scriber registrate uso, maximo populos linguistas es sia. O integre presenta sia.",
                "E per human libera cadeva, vide instruite grammatica duo in? Campo altere encyclopedia con se. Esseva introduction se sia. O medio original specimen nos, que inviar encyclopedia tu. Ma debe existe personas uso. Lo nos asia original, celo inviar que se, in via libere durante.",
                "De maximo summarios primarimente que. Cinque latino cadeva un web, in sed lista terra! O studio involvite greco-latin qui! Tu integre articulo historiettas sia?",
                "Es scientia vocabulos con? Laborava debitores technologia que al. Non le scientia historia, sitos mundial lo sia! Non ma membros subjecto, per un russo parola secundo. Malo regula historia ha sia, nomine europee per lo, e americas proposito per?",
                "Ha uso quales europa linguage, via toto tentation sanctificate al! Con africa avantiate un. Via denominator anteriormente se, initialmente introductori se uso, que personas publicate auxiliary se. Ha nos auxiliar initialmente.",
                "De giuseppe traduction linguistic nos. Del il russo articulo interlingua, ultra traduction anteriormente sed al! Le con avantiate involvite, unic latente proposito uso se, periodicos primarimente con o. Tu per unic lista. Pan practic publicate lo, illo medio complete web al?",
                "Titulo synonymo nos un. Tu anque linguas nos? Qui servi vices ha. Web lista deler facite e, lingua subjecto uno un. Flexione effortio publicava un con, del ha peano parlar philologos.",
                "Il lingua europa scientia uso? Summario questiones sed un? Del o toto existe westeuropee, religion abstracte interlingua del il? Mundial populos svedese un per. Per tote sitos ma, ha existe registrate paternoster via. Sed un philologos millennios, con ma technic secundo giuseppe.",
                "Un usos asia ascoltar qui, super parola debitores ha web. Westeuropee interlingua essentialmente qui tu, uso o ultra terra professional, linguas debitores in qui. Se nos vista synonymo technologia. Da uso celo post rapide, vices integre tu sed. Uno campo svedese al?",
                "Nunc tempus felis ac est rhoncus placerat. Nulla dolor urna, lobortis ac sem in, efficitur suscipit dui. Cras tristique molestie placerat. Pellentesque mattis dolor in neque posuere lacinia. Aliquam nec condimentum eros. Sed dictum quam felis, ac pharetra sem finibus ut. Donec consectetur neque ornare diam rutrum egestas. Nunc aliquet cursus tristique. Phasellus non dui id erat malesuada luctus eu vel risus.",
                "Vestibulum gravida, ligula eu cursus consequat, dolor mauris tristique felis, condimentum sollicitudin magna leo dapibus purus. Duis euismod urna id volutpat pellentesque. Morbi mattis turpis id feugiat pellentesque. Donec quis felis elementum, elementum velit a, sagittis dui. Nulla facilisi. Curabitur id condimentum lectus. Donec ut magna elementum, dapibus augue id, sagittis enim. Donec vitae tempus enim, vel tempus nisl. Vivamus facilisis rutrum ultrices. Morbi ex sapien, ultrices eu ex ultricies, iaculis hendrerit ipsum.",
                "Nam vulputate enim a leo convallis, ut eleifend elit malesuada. Nullam porttitor neque arcu, eget interdum magna interdum at. Nunc aliquet mauris dolor, in pretium ligula dapibus eget. Praesent eu pellentesque mi, a semper magna. Vivamus tristique nisl sit amet dolor facilisis sagittis. Sed efficitur vel ligula at finibus. Morbi at leo risus. Phasellus vel leo felis.",
                "Sed tincidunt justo id tortor ultrices porttitor. Donec orci turpis, vehicula eget sodales et, tempus a est. Fusce in libero id leo congue eleifend sit amet interdum nisi. Nam molestie tincidunt euismod. Cras eget rutrum nibh, non dictum libero. Quisque at tempor lacus. Donec et nulla mollis, feugiat tellus vitae, iaculis nibh. Morbi maximus a libero in interdum. Praesent vitae enim feugiat mi viverra gravida nec sit amet nisl.",
                "Nunc eget magna augue. Cras congue velit eu dui mattis ultrices. Vivamus a tristique ex, sit amet pharetra lorem. Vestibulum sagittis, sem feugiat gravida ornare, dui felis efficitur erat, eget scelerisque nulla nibh eget nulla. Nam non urna metus. Ut sed diam eget massa congue condimentum congue sit amet ligula. Suspendisse potenti. Morbi mauris nisl, imperdiet eu venenatis dignissim, iaculis et ante. Cras eu cursus ligula. Quisque felis quam, volutpat sed quam et, posuere pulvinar nunc. Curabitur luctus, sapien sed imperdiet vulputate, ex felis tempor est, nec interdum ante tellus in leo. Sed eleifend vel mauris vitae luctus. Praesent et mi ut dui suscipit rhoncus. Integer hendrerit malesuada ante, id malesuada diam euismod at. In tempus neque ut ante dapibus, vitae dapibus turpis consectetur.",
                "Duis iaculis ligula tortor, vel pharetra risus iaculis in. Aliquam faucibus commodo justo quis maximus. Nullam ut risus posuere, vehicula ante ut, volutpat lorem. Etiam nec luctus ante, eget lobortis sem. Integer pellentesque orci erat, eu porttitor nibh bibendum vel. Mauris eu ipsum sit amet elit facilisis tempor non et justo. Pellentesque molestie interdum quam, et faucibus purus molestie sit amet. Quisque posuere consequat risus, at laoreet risus ultricies quis. Aliquam suscipit leo elementum nulla luctus molestie. Suspendisse quam est, faucibus eget eros at, dignissim auctor nulla. Sed aliquet elit dui, non ullamcorper felis dapibus sit amet. Sed convallis vulputate nulla, et vestibulum urna facilisis vel. Morbi in libero dolor. Curabitur id pellentesque arcu. Nulla efficitur auctor congue. Aenean et pulvinar magna, vel ullamcorper nunc.",
                "In nec velit molestie, fringilla risus in, pellentesque magna. Nam eget purus at neque suscipit fringilla. Duis quis fringilla purus. Pellentesque elit mauris, gravida ut elit non, malesuada iaculis tellus. Maecenas in elit lorem. Sed laoreet sem eget leo gravida, volutpat ultricies erat sodales. Nulla dictum cursus cursus. Morbi non consequat nulla. Vestibulum nec odio eu turpis tincidunt posuere nec non metus.",
                "Sed maximus neque quis est volutpat luctus. Cras in enim a est aliquam egestas. Integer varius ac magna et dapibus. Integer vel condimentum libero. Fusce tempus dui quis finibus pretium. Vestibulum fermentum eleifend ornare. Nullam non velit ultricies, ultricies massa ac, tempor massa. Donec accumsan massa vitae nunc mattis bibendum. Donec a tortor pretium, tempus tortor non, feugiat dolor. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Morbi ornare sollicitudin velit, vitae sollicitudin lacus lacinia quis. Morbi vulputate maximus neque ut varius. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Nam elementum non risus sed vestibulum. Phasellus a elementum erat. Donec ornare neque eu neque iaculis, vitae sodales dolor sagittis.",
                "Donec at pharetra augue, et pretium est. Donec nibh magna, iaculis ut auctor sed, cursus non sapien. In eget pharetra lectus, ut accumsan enim. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Maecenas quis tellus mattis, gravida eros vitae, rutrum nunc. Pellentesque non dapibus nisl, posuere congue diam. Aenean lobortis sagittis turpis et hendrerit. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Integer at sollicitudin lorem. Mauris ut ornare ipsum. Donec viverra orci in mi ultricies dapibus ac at magna. Pellentesque efficitur sem turpis, a posuere nunc semper euismod. Ut ullamcorper erat in feugiat mattis. Maecenas ipsum nulla, tincidunt a dolor eget, congue laoreet dolor. Vestibulum interdum ex non fermentum fermentum.",
                "Aenean in vehicula diam. Nam eu turpis erat. Nullam luctus accumsan ipsum, in tincidunt lectus tempus eget. Interdum et malesuada fames ac ante ipsum primis in faucibus. Duis non condimentum magna, quis cursus diam. Pellentesque lorem mauris, pellentesque nec orci at, luctus imperdiet sapien. Nam imperdiet neque a eleifend molestie. Phasellus malesuada, ligula sit amet porttitor gravida, mi nisl eleifend nisi, sed facilisis lacus purus ut tortor. Sed gravida mollis urna et ultricies. Vivamus convallis libero nec orci interdum, luctus rhoncus eros tincidunt. Pellentesque tempus vehicula metus vel porta. Nulla facilisi. Nam mattis tortor vel faucibus gravida. Proin eleifend justo vel quam gravida, quis consequat lacus imperdiet. Vestibulum molestie lacus sed risus rhoncus, eget venenatis urna accumsan.",
                "Maecenas ullamcorper nulla nec lorem suscipit, non euismod ante lobortis. Nunc iaculis iaculis nunc a eleifend. Nullam tincidunt metus eu sem faucibus sagittis. Suspendisse egestas mauris vel turpis interdum, a finibus urna finibus. Aliquam ultrices quis sapien iaculis porttitor. Vivamus efficitur tincidunt nisl nec venenatis. Pellentesque dignissim, lorem eu ullamcorper tempor, libero velit gravida ligula, non consequat metus tortor ac urna. Nam vehicula justo arcu, sit amet sollicitudin risus porttitor in. Donec et nisi at tortor placerat imperdiet. Quisque finibus elit in ipsum consequat, et pulvinar lorem sollicitudin. Donec a feugiat ipsum. Sed venenatis ornare dolor, eget tempus metus malesuada ut. Vivamus vitae sagittis neque.",
                "Cras ante ipsum, molestie id orci ut, luctus placerat metus. Vivamus dignissim, nibh nec luctus vestibulum, purus lorem posuere leo, vitae rhoncus ligula arcu eu mauris. Fusce consequat quis felis faucibus interdum. Sed tincidunt quam a lacus commodo imperdiet. Vestibulum elementum sagittis fermentum. Donec imperdiet eros consectetur odio rhoncus finibus. Interdum et malesuada fames ac ante ipsum primis in faucibus. Sed non arcu quis ipsum ornare pretium.",
                "Nam mollis dapibus consequat. Vestibulum eget nibh ullamcorper, iaculis augue non, suscipit justo. Integer blandit cursus erat, eu efficitur lacus dictum a. Pellentesque vitae rhoncus risus. Nunc et accumsan felis. Nullam hendrerit arcu quis accumsan convallis. Aenean euismod orci nec velit efficitur gravida. Duis lacinia nibh et velit tincidunt, a hendrerit tortor sollicitudin. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris accumsan et ante porttitor ullamcorper. Aliquam nec justo vitae sem varius eleifend ut ac justo. Sed at neque orci.",
                "Nam enim tortor, dictum ac ipsum quis, scelerisque bibendum orci. Nunc a lectus auctor, laoreet neque ac, volutpat neque. Quisque eget ligula a arcu consequat condimentum. Quisque elementum ultricies aliquam. Suspendisse nec felis sem. Nunc faucibus velit sit amet consectetur iaculis. Fusce quam nisl, tristique et convallis sit amet, consectetur a sapien. Mauris turpis ligula, semper a purus a, porta consequat ex. Suspendisse porta lacus molestie, efficitur lorem ac, ornare erat. Pellentesque lobortis urna eu metus condimentum suscipit. Pellentesque interdum viverra accumsan. Aliquam imperdiet diam quis magna volutpat condimentum. Nam condimentum, nisl vel interdum mollis, ligula est blandit turpis, commodo hendrerit lorem dui eleifend neque.",
                "Morbi euismod sit amet sapien ut sodales. Integer iaculis maximus elit ut laoreet. Phasellus eget tellus id ex semper placerat vitae vel orci. Sed porta dolor a risus facilisis, efficitur bibendum mi consectetur. Aenean fermentum aliquet dui vel pulvinar. Vivamus non lobortis quam. Morbi imperdiet rhoncus eros, eget mattis arcu. Ut at tincidunt leo, at aliquet nibh. Phasellus a ultricies leo. Morbi et consequat sapien. Curabitur dictum, nibh a ultrices cursus, purus nisi pharetra est, ac mollis velit nulla eget urna. Sed massa risus, pretium at elit eget, convallis ultricies nisl. Vivamus luctus aliquet pharetra. Donec tincidunt quam turpis, ac dignissim mi faucibus sed. Sed a facilisis sapien, sed interdum nunc. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.",
                "Phasellus quis nibh nec lacus hendrerit consequat. Vivamus et mauris vel lectus sagittis cursus nec facilisis est. Nunc eget sapien iaculis, tempus ex at, cursus sem. Vestibulum venenatis leo quis tortor fermentum, sed sodales erat vehicula. In hac habitasse platea dictumst. Sed pellentesque venenatis ullamcorper. Vestibulum sed porttitor libero. Fusce non urna nulla.",
                "Proin nec purus non diam luctus dictum sed eget lacus. Nulla suscipit elementum molestie. Integer id interdum purus, eu accumsan leo. Suspendisse eget auctor nisl, sed congue justo. Duis rutrum elit in orci rutrum pulvinar. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Sed suscipit diam at pretium interdum. Aliquam finibus felis arcu. Donec rutrum tellus non ultricies pulvinar. Proin feugiat, urna non vestibulum varius, ligula orci tristique ante, ac mollis tellus diam eu velit. Sed fermentum dui sapien, et pretium lectus ultrices at. Ut sed dignissim nibh, sit amet rutrum diam. Mauris placerat lorem quis dolor venenatis, ac scelerisque nunc dignissim. Etiam a rutrum arcu. Duis ac massa vel nunc tincidunt facilisis consectetur porta turpis. Ut semper luctus turpis.",
                "Sed non feugiat quam. Integer rhoncus a nisl at faucibus. Quisque venenatis odio velit, non molestie lorem cursus vel. Phasellus a risus sem. Aliquam eu diam sem. Vivamus odio urna, efficitur quis eros a, porta interdum mauris. Praesent interdum sollicitudin mattis.",
                "Donec in consectetur urna. Proin eleifend, nunc vitae bibendum eleifend, mi neque hendrerit nibh, vitae commodo neque tellus sit amet massa. Mauris faucibus nisi urna, et consequat ante interdum eget. Suspendisse quis vulputate nulla, sit amet maximus eros. Aliquam convallis vel enim vel placerat. Suspendisse potenti. Duis fringilla augue at enim facilisis, ut convallis justo tempor. Praesent tincidunt odio diam, quis gravida nisi pellentesque a. Nam sodales ante nec efficitur sollicitudin. Vivamus congue tortor urna, et tincidunt mi semper non. Praesent neque justo, fringilla vel mollis vitae, sagittis vel leo. In sed risus ornare, auctor ligula a, rhoncus urna. Maecenas blandit eros felis, vitae consectetur lacus sagittis in. Fusce pretium sapien orci, in posuere nunc pulvinar ac.",
                "Nullam lobortis metus fermentum rutrum placerat. Morbi elementum felis vitae velit vehicula, pretium laoreet diam egestas. Quisque laoreet ligula in ante venenatis ultrices. Sed vitae sapien non eros tristique ultricies sed vitae nibh. Duis quis placerat eros. Sed sit amet posuere odio, eu consequat nulla. Donec sit amet ex commodo, tempor urna id, tincidunt eros. Mauris imperdiet eget velit ac commodo. Etiam fermentum bibendum leo quis suscipit. Vivamus finibus ex eleifend accumsan lacinia. Curabitur a auctor enim. Maecenas eu pharetra nisl, ut molestie arcu. Nunc placerat augue rhoncus erat varius maximus. Sed sodales ex massa. Quisque eu diam euismod lorem scelerisque bibendum.",
                "Vestibulum dapibus ornare nunc nec tempor. Maecenas at est id ipsum efficitur pharetra nec a diam. Integer ut eros sed diam laoreet dictum. Morbi eu condimentum ligula. Morbi semper dapibus turpis, pellentesque cursus quam bibendum tincidunt. Aenean ultrices velit et nisi rutrum, et bibendum odio ultricies. Suspendisse quis lectus vel ipsum mattis varius malesuada at magna. Interdum et malesuada fames ac ante ipsum primis in faucibus. Maecenas aliquet a lacus id elementum. Interdum et malesuada fames ac ante ipsum primis in faucibus. Morbi lectus lorem, tempor eget aliquam non, elementum fringilla lorem. Nam vestibulum et urna ac sagittis. Etiam a dui non sapien convallis malesuada. Quisque sed massa consequat odio ultricies tempus.",
                "Aenean accumsan suscipit commodo. Donec at eros diam. Aenean hendrerit tristique malesuada. Curabitur eu convallis risus, at posuere nisi. Maecenas a nunc tellus. Nam porttitor nisi quis nisi bibendum, vel venenatis justo tempor. Sed at pharetra lacus. Cras nec euismod ipsum.",
                "Praesent id tristique sapien. Vivamus mollis sagittis sapien, sed varius sapien ullamcorper ac. Duis commodo ipsum ac justo pretium, pharetra eleifend leo ultrices. Integer ex tellus, scelerisque id bibendum sed, feugiat vel neque. Curabitur dapibus urna a massa fringilla placerat. Aliquam nisi lorem, condimentum id auctor sed, porttitor ut tortor. Sed ac efficitur risus, vel pharetra magna. Quisque scelerisque eros vitae est dictum tempus. Integer quis bibendum neque. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Ut tempor purus non lacus elementum, vitae bibendum purus dignissim. Suspendisse potenti. Fusce elementum nunc eget commodo pretium. Nulla elit turpis, iaculis nec hendrerit et, eleifend vel dolor. Mauris urna quam, iaculis at nunc convallis, porttitor eleifend sem.",
                "Suspendisse finibus et tortor quis interdum. Aenean odio massa, iaculis quis justo sed, mattis mattis metus. Morbi dictum est blandit lorem luctus pretium sed vitae purus. Proin volutpat semper nunc ac finibus. In mauris ante, dignissim sit amet fermentum sit amet, lacinia id sem. Sed vel venenatis tortor. Duis vitae finibus sapien, vitae porttitor sapien. Praesent interdum commodo est, vel auctor magna venenatis ac.",
                "Etiam pellentesque tempor tempus. Maecenas aliquam rhoncus accumsan. Nullam sit amet quam nec mauris scelerisque tincidunt ac efficitur orci. Vivamus convallis nibh ac mauris gravida consequat quis ac tortor. Ut vel libero massa. Vestibulum viverra nisi ac mi tempor, quis euismod nulla tempus. Nullam pulvinar vestibulum mauris, eget tempor dolor scelerisque sed. Mauris sit amet sagittis mi, a varius lacus. Proin viverra sem libero, id consequat dui rhoncus sed. Aliquam feugiat pellentesque libero, vel maximus lacus eleifend et. Duis dui neque, consequat vitae nibh at, laoreet rhoncus ipsum. Curabitur posuere tellus eget aliquam efficitur. Etiam dignissim pretium urna, eu dictum turpis porta in.",
                "In id placerat tellus, non faucibus augue. Curabitur venenatis pellentesque neque, ac mollis risus feugiat sed. Sed gravida nisl eu ligula ultricies blandit. Nunc est mi, dapibus nec risus vel, sagittis condimentum lorem. Sed venenatis quam libero, quis malesuada mi ultrices et. Suspendisse tincidunt ligula at egestas molestie. Aliquam consequat laoreet lorem eu tristique. Donec ac nunc auctor, pellentesque lectus vel, blandit turpis. Fusce orci metus, elementum at finibus vitae, varius ut tortor. Aliquam viverra ullamcorper efficitur.",
                "Nunc eu laoreet nulla, sit amet posuere odio. Curabitur dolor lorem, pulvinar venenatis mollis in, malesuada eget massa. Suspendisse dolor leo, iaculis ut hendrerit posuere, maximus id sapien. Aenean quis libero nec ante ullamcorper venenatis et vel ex. Cras lobortis at metus sit amet consequat. Integer in scelerisque felis, et ultrices purus. Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                "Vivamus volutpat diam eget est lobortis venenatis. Morbi cursus tellus a aliquet consequat. Etiam cursus, tellus vitae vulputate consequat, massa magna convallis tortor, nec eleifend massa mauris ac quam. Fusce eget tempus augue, sed tincidunt leo. Donec ut tristique orci. Aliquam placerat, tellus sed fringilla gravida, sapien lorem blandit est, et rutrum est ipsum vitae odio. Integer quis convallis lectus. Sed et magna sit amet augue semper pretium. Duis ornare arcu orci, et tempus massa semper a. Ut eget eros eros. Suspendisse potenti. Ut felis leo, facilisis eu tortor suscipit, mollis sodales felis. Nullam sollicitudin leo quis magna blandit efficitur.",
                "Aliquam placerat erat ut mi fringilla ultrices. Suspendisse eu metus in odio luctus euismod. Phasellus efficitur rhoncus eleifend. Suspendisse potenti. Morbi eget nisi et nisl ornare tincidunt porttitor et dolor. Fusce tristique velit sit amet arcu malesuada, sed consectetur metus auctor. Curabitur in arcu non massa tristique pharetra eget at ante. Cras interdum odio eget hendrerit interdum. Nunc facilisis nunc sed nibh placerat ornare. Etiam pharetra neque ut arcu efficitur, interdum pulvinar nibh sollicitudin. Sed rutrum turpis sit amet aliquet vulputate. Curabitur non elit urna. Duis pharetra, orci id pellentesque sagittis, justo libero varius tortor, in vehicula dolor nibh ac orci. Aenean sodales eros sed purus sollicitudin blandit. Phasellus ac ex mi. Praesent fringilla quis elit vel venenatis.",
                "Fusce turpis risus, ultricies sed ullamcorper nec, hendrerit vel elit. Donec cursus arcu eu quam porta iaculis. Aenean aliquet egestas porttitor. Nam est tortor, volutpat ac mollis sit amet, suscipit quis arcu. Morbi ac dui interdum, ultrices odio ut, auctor tortor. Vestibulum et vehicula dui, ut porta orci. Morbi volutpat lacus ac mauris posuere laoreet. Cras molestie velit in rutrum egestas. Nunc tristique felis non mi suscipit, nec malesuada nunc pharetra. Fusce sodales dui massa, eu ultricies massa malesuada vitae. Nam maximus eleifend augue ac sagittis. Aliquam molestie a mauris ut dignissim.",
                "Maecenas vel ipsum facilisis, luctus erat viverra, pulvinar mauris. Cras consectetur dolor et porttitor finibus. In eu blandit nulla. Nam in dui eget nulla viverra aliquet ac eget metus. Vestibulum at ipsum dapibus, molestie nibh pulvinar, vestibulum massa. Integer commodo pharetra dui vel facilisis. Sed neque dui, condimentum eget accumsan sed, hendrerit sed diam. Fusce ac nisi a metus sodales lobortis non quis mauris.",
                "Praesent pharetra nulla eros, ultrices pellentesque elit gravida in. Aliquam finibus cursus odio, sed sodales neque maximus ac. Proin sit amet condimentum ligula. Ut laoreet nisl sed arcu aliquet, non convallis massa pharetra. Ut maximus laoreet magna. Duis elementum urna ac rhoncus tincidunt. Mauris sit amet euismod urna, sed porta nisi. Duis congue orci ac nisl faucibus, vel pharetra ligula sodales. Nulla volutpat urna sed mauris suscipit, et scelerisque turpis hendrerit. Maecenas interdum felis laoreet ante luctus consequat. In hac habitasse platea dictumst. Curabitur non sapien vitae ligula consequat dapibus. Proin ut venenatis purus. Sed sollicitudin nulla et est convallis, sed sollicitudin tellus cursus. Maecenas venenatis nulla in libero dapibus convallis congue vel erat. Mauris condimentum augue quis ex venenatis euismod.",
                "Phasellus quis metus magna. Suspendisse consectetur, odio vel rutrum lacinia, nisl felis rhoncus nulla, non placerat velit mi sed sapien. Donec dapibus lorem id sapien pulvinar, vel blandit ipsum pulvinar. Nam volutpat tellus vitae feugiat imperdiet. Duis efficitur magna massa, et facilisis erat blandit non. Praesent gravida velit ut consequat pharetra. Sed egestas arcu quis libero facilisis, vel suscipit sem hendrerit. In mattis lectus id nisl suscipit molestie. Vivamus volutpat, est ac tempor rutrum, quam velit commodo nunc, sed commodo ipsum risus ut augue.",
                "Nam tristique pharetra eros, id cursus velit. Sed malesuada magna ac neque faucibus, et blandit urna porttitor. Nam tristique congue rutrum. Sed vel ultricies mi, a semper leo. Ut a hendrerit leo, at venenatis orci. Aliquam erat volutpat. Nulla sed nulla eu velit interdum volutpat. Sed semper lorem ac euismod egestas. Integer diam enim, lobortis ut malesuada et, pulvinar vel magna. Praesent tempus nibh turpis, ac maximus nisl euismod at. Sed mattis, eros ac dignissim mattis, ligula nibh luctus odio, sit amet tincidunt eros justo eget elit. Integer ut nisl dui. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae;",
                "Fusce egestas, nisl id malesuada tincidunt, nulla libero placerat tellus, in tempor augue felis eget justo. Etiam ornare nibh eget porttitor molestie. Pellentesque nec eros mauris. Nunc id risus a eros lacinia eleifend. Nullam volutpat diam vel pellentesque maximus. Maecenas pellentesque leo at diam pulvinar faucibus. Nam feugiat id leo lobortis placerat. Sed id tortor in metus luctus dictum. Mauris molestie ligula id purus bibendum tincidunt. In auctor condimentum est vitae laoreet. Curabitur fermentum eget nulla convallis sodales. Nunc rhoncus venenatis dictum.",
                "Vestibulum sit amet vestibulum tellus. Donec ultricies consectetur luctus. Aliquam risus ipsum, eleifend et tempor vitae, cursus in turpis. Ut eget erat sed ante consequat sagittis ut nec magna. Sed vitae condimentum lectus, a dictum sem. In elit est, interdum fermentum massa vel, fringilla consectetur tellus. Pellentesque et est nec arcu egestas bibendum. Vestibulum tempor sapien eget augue fringilla eleifend. Vestibulum placerat magna sit amet arcu interdum, eget cursus augue blandit. Vestibulum elementum consequat dignissim. Aliquam imperdiet ex eu libero aliquam imperdiet. Etiam eget nulla et lectus eleifend venenatis. Curabitur accumsan neque erat, a laoreet enim semper vitae.",
                "Nunc pretium nisl felis, ac euismod odio mollis vitae. Morbi volutpat, quam tempus dictum feugiat, mi sapien fermentum elit, at maximus tortor nisl a nisl. Mauris sed nunc odio. Cras a lacus pharetra, convallis lectus in, imperdiet lectus. Cras vulputate sodales urna nec mollis. Duis et velit sapien. Nulla facilisi. Ut fringilla libero nec est finibus iaculis. Donec sed mi eu lacus pretium laoreet sit amet sit amet purus.",
                "Phasellus libero est, vulputate volutpat quam sit amet, faucibus auctor leo. Phasellus maximus metus vel laoreet congue. Ut ut varius ipsum. Fusce in dolor auctor, consequat tortor ac, consectetur est. Mauris vestibulum efficitur congue. Aliquam tincidunt sodales dolor. Proin nec orci ultricies lacus iaculis lobortis. Curabitur tortor mauris, faucibus ut erat nec, maximus vestibulum sapien. Etiam vitae semper metus. Suspendisse elementum nisi eu interdum tincidunt. Mauris imperdiet porttitor enim. Donec volutpat leo vitae purus varius, ut hendrerit arcu vehicula. Etiam posuere tortor magna, ut suscipit erat porttitor vel. Nullam aliquam metus nec nunc convallis congue.",
                "Fusce vitae massa id tellus eleifend venenatis in pharetra orci. Vivamus suscipit orci in justo congue, ac accumsan velit ullamcorper. In sed dolor et mauris laoreet facilisis. Donec eu auctor orci, laoreet hendrerit tortor. In nec turpis sed ex mollis vestibulum. Nam mollis volutpat ex vel luctus. Nunc quis felis eget orci mattis interdum ac quis nulla. Maecenas in enim semper magna tempus mattis et a ex. Praesent odio eros, interdum ut sollicitudin dictum, aliquam in ex. In luctus elit non tortor rhoncus, vitae faucibus urna tincidunt. Donec at sem semper, bibendum odio sit amet, posuere justo. Fusce non purus dapibus, commodo ante ut, faucibus leo. Suspendisse porta urna id orci consequat aliquam. Integer non aliquam tortor. Proin ut cursus nibh. Phasellus porta purus eu nisi tempor, ac aliquet dolor volutpat.",
                "Etiam ultricies eget lorem eget ultricies. Morbi malesuada, mi ultricies ullamcorper fringilla, ex lacus lacinia dui, a gravida ligula odio vitae magna. Aenean varius, ante vel consequat pharetra, quam dolor elementum ipsum, in euismod tortor sem vel orci. Vestibulum eget orci non nibh vulputate laoreet venenatis ut odio. Donec porttitor magna ut purus sagittis, eu accumsan leo tempus. Pellentesque eu massa tincidunt, facilisis eros quis, auctor dui. Duis magna risus, iaculis at quam sed, scelerisque sodales nisl. Interdum et malesuada fames ac ante ipsum primis in faucibus. Sed eu augue vitae est pulvinar pellentesque eu vehicula nulla.",
            };
            return contents[rnd.Next(0, contents.Count)];

        }
        // special thanks to the team of http://lipsum.com/
        private string GetRandomTitle()
        {
            var titles = new List<string>()
            {
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
                "Nulla accumsan velit sit amet eros tempus, vitae luctus augue porta",
                "Aliquam efficitur purus a sagittis mattis",
                "Sed sit amet ipsum accumsan, lobortis nibh quis, pharetra ante",
                "Nullam ac ligula eget nibh volutpat tempor",
                "Morbi mollis urna non enim luctus facilisis",
                "Integer sit amet neque eget tellus blandit tempor vitae dapibus enim",
                "Morbi sagittis tortor a lobortis porta",
                "Ut vitae sem eu mauris dictum blandit at vel turpis",
                "Donec eu urna scelerisque, mattis tellus gravida, vehicula nisl",
                "Aenean sit amet mauris sit amet libero congue dictum vel a odio",
                "Suspendisse luctus tortor nec faucibus dictum",
                "Vestibulum nec mauris eu elit imperdiet finibus in a arcu",
                "Quisque semper eros ac justo luctus, ac consequat nisl condimentum",
                "Sed varius enim quis pulvinar interdum",
                "Aliquam sed eros a ipsum cursus facilisis",
                "Aliquam venenatis leo sit amet efficitur sagitti",
                "Nunc hendrerit libero eget malesuada cursus",
                "Aenean vel neque in massa tristique bibendum",
                "Proin non diam ut nulla tempus tristique ut sit amet diam",
                "Praesent quis velit vitae orci vehicula semper",
                "Quisque vel justo sed leo tempus vehicula ac at turpis",
                "Sed sit amet urna quis ex blandit molestie",
                "Quisque vel sem auctor, posuere nisi eu, egestas mi",
                "Morbi nec mauris id odio consectetur laoreet",
                "Donec ac ex eu felis ornare tempor sed sit amet enim",
                "Fusce nec justo sit amet purus lacinia tempor sed et metus",
                "In dictum lectus ut lectus imperdiet, eget euismod urna elementum",
                "Donec bibendum eros nec nisl imperdiet, et egestas nunc lobortis",
                "Donec egestas lacus ut ligula interdum, ut egestas augue rutrum",
                "Sed porttitor erat at ipsum tincidunt, id interdum lacus consectetur",
                "Aliquam a lectus lacinia, posuere magna vitae, placerat neque",
                "Pellentesque et dui eu nulla tempor hendrerit a molestie massa",
                "Ut vulputate augue at metus tempus tristique",
                "Suspendisse posuere dui sit amet aliquam pretium",
                "Sed in nibh bibendum, mollis nisl id, vestibulum massa",
                "Vestibulum non ante id ante tempus varius in vel felis",
                "Morbi bibendum nisl eget mauris vestibulum, sit amet pellentesque odio tincidunt",
                "Donec nec arcu vel mi laoreet rhoncus et sed neque",
                "Phasellus sit amet risus posuere, condimentum libero ut, tristique massa",
                "Phasellus in massa tincidunt, consequat nulla quis, viverra lacus",
                "Nam et dolor eget nisi tempus posuere eu in tellus",
                "Morbi at felis nec ante dictum scelerisque",
                "Aliquam scelerisque sem eu tellus cursus semper",
                "Nulla eget dolor luctus, condimentum lorem a, hendrerit est",
                "Donec mattis neque vitae augue varius, vel bibendum nulla fringilla",
                "Curabitur posuere nibh eu sollicitudin rutrum",
                "Quisque feugiat lectus at euismod maximus",
                "Praesent ac nunc suscipit, fermentum libero vel, ultrices turpis",
                "Quisque tristique nisi id metus tempor gravida",
                "Cras et lectus fringilla ex venenatis blandit in quis nunc",
                "Fusce id ipsum ac velit dapibus euismod",
                "Fusce non quam eget mi pulvinar ultrices non eu ante",
                "Phasellus a mauris quis lorem pretium elementum a sed nisi",
                "Praesent vitae arcu luctus, rutrum tellus bibendum, tempor sem",
                "Duis nec odio at neque luctus molestie",
                "Suspendisse pulvinar risus et lectus ullamcorper porttitor ut ac augue",
                "Nam id ex cursus, scelerisque dolor sit amet, efficitur est",
                "Proin sed elit quis dui ultricies facilisis eget vitae arcu",
                "Nulla rutrum est at sem semper venenatis",
                "Vestibulum id ligula quis est pellentesque cursus eu ut augue",
                "Duis cursus purus eget augue auctor luctus",
                "Maecenas venenatis metus eu tristique venenatis",
                "Duis in turpis tincidunt, tincidunt nulla vulputate, interdum dui",
                "Nam vitae orci quis lorem congue pretium",
                "Nullam faucibus dolor vitae ante pharetra, id ornare nisi pharetra",
                "Sed eget augue ac mi tempor porta",
                "Phasellus id mi semper, faucibus erat vitae, ornare mauris",
                "Proin et ipsum tincidunt, egestas diam ut, lobortis justo",
                "Fusce eget sem congue, egestas urna eu, maximus lacus",
                "Curabitur ut est eu purus ornare ultrices nec a sem",
                "Ut accumsan ante sit amet elit dignissim, in facilisis tortor accumsan",
                "Ut vel mauris nec mi vehicula luctus sit amet vitae orci",
                "Donec convallis risus molestie imperdiet tempus",
                "Fusce nec justo eget velit accumsan accumsan ut eu elit",
                "Morbi non enim imperdiet, semper lectus nec, fringilla urna",
                "Morbi in massa eget orci posuere eleifend",
                "Cras convallis purus et nibh ultricies tincidunt",
                "In quis nisi vitae odio sagittis aliquet",
                "Ut vel lacus non ligula tincidunt egestas",
            };

            return titles[rnd.Next(0, titles.Count)];
        }

        private string GetRandomParagraph()
        {
            var paragraphs = new List<string>()
            {
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse eleifend turpis quis convallis consectetur. Donec id lorem vehicula, tincidunt quam vel, elementum eros. Nulla nisl urna, gravida quis ullamcorper in, semper eget lorem. Donec nec dolor facilisis sem viverra pellentesque. Sed lorem urna, luctus et elit sed, posuere fringilla risus. Aliquam rhoncus tellus non fringilla finibus. Sed pharetra in neque eu pulvinar. Sed dapibus felis a vulputate volutpat. Curabitur nisl nulla, aliquam ut risus quis, luctus hendrerit ante. Integer vel libero dictum, vulputate odio et, laoreet purus. Suspendisse dignissim libero nec sem tristique, a facilisis massa ultricies. Cras vitae maximus ipsum. Maecenas et pulvinar urna.",
                "Nunc bibendum ipsum et ex feugiat porta ut nec ante. Duis tempus diam erat, a pellentesque nunc varius vitae. Nullam iaculis bibendum justo tempor sollicitudin. Etiam at accumsan sapien, luctus sollicitudin quam. Morbi sagittis sagittis diam, quis sodales lacus luctus at. Aenean rutrum urna in ligula cursus maximus. Nam magna lacus, iaculis rutrum lorem sit amet, lobortis mollis tortor. Duis imperdiet condimentum est quis tempor. Nullam a libero sit amet odio fringilla pulvinar nec in tortor. Donec dictum mauris vel augue auctor, sed imperdiet ipsum pellentesque. Cras turpis nisl, fringilla at hendrerit nec, accumsan sit amet lacus.",
                "Nullam lacinia quam diam, ut maximus nibh pretium eu. Sed dui massa, ornare eu semper efficitur, convallis id eros. Donec faucibus lectus et elit ultrices, ut hendrerit libero congue. Donec vel sapien vel dui dignissim tincidunt imperdiet vel tortor. Cras vel fermentum sem, nec vestibulum libero. Morbi commodo pellentesque velit, id luctus neque dapibus lacinia. Vivamus a magna tempor, tristique est quis, egestas odio. In sit amet convallis ex, non consectetur est. Pellentesque et imperdiet ligula. Sed erat massa, mollis nec consectetur et, varius in mi. Aenean lectus tellus, tempor id gravida ac, pulvinar et libero. Mauris eu vestibulum quam. Pellentesque ultricies lorem sit amet risus commodo auctor.",
                "Morbi varius, tortor sit amet porta finibus, ipsum enim aliquam quam, sollicitudin mollis leo nisl vel ipsum. Donec tincidunt, magna vitae auctor tempus, nulla orci lobortis massa, ac sollicitudin urna massa non odio. Nulla est neque, volutpat et nisl eu, ullamcorper ultrices magna. Nunc sodales rhoncus diam, placerat pulvinar nulla pulvinar quis. Ut quis turpis ultrices, placerat nunc eu, gravida tortor. Suspendisse hendrerit at neque et mattis. In ornare in leo id vulputate. In sit amet nibh ut velit ornare congue ut non lacus. Donec sit amet felis sagittis, lacinia lectus a, tempor ante. Curabitur tempor dictum tellus, vel ultricies leo lacinia laoreet. Pellentesque feugiat, eros non tincidunt maximus, nibh quam aliquam nisl, sed ullamcorper lacus diam nec ex. Vivamus nec quam sit amet enim sollicitudin mollis quis at velit. Vestibulum imperdiet pellentesque eros, sed vulputate ipsum condimentum eu. Pellentesque molestie faucibus elit eu porta. Praesent laoreet non sapien et efficitur. Praesent ut metus sit amet justo fringilla feugiat vel ac sem.",
                "Ut orci tellus, fringilla ut libero id, mollis ultrices purus. Praesent ornare dolor magna, vel tempor mi scelerisque ut. Praesent eu justo erat. Mauris lobortis dui eget mollis lobortis. Nam facilisis dapibus dolor, vel interdum elit sollicitudin a. Donec dapibus ultrices purus quis accumsan. Suspendisse diam est, luctus in lacus in, tincidunt faucibus quam.",
                "Duis in eros eget metus ullamcorper scelerisque sed ac magna. Proin ac convallis neque, sit amet placerat libero. Proin at risus interdum, malesuada justo eget, blandit sem. Ut tempor erat vitae sapien eleifend varius. Sed id neque sit amet lorem lacinia pretium. Phasellus in lacus congue ligula scelerisque porta. Phasellus consectetur tortor vitae magna molestie vehicula. Donec efficitur, diam feugiat sollicitudin mattis, augue augue sodales purus, at volutpat ligula magna et sem. Morbi pharetra libero id euismod mattis. Donec ullamcorper aliquam turpis porta vulputate. Donec fringilla laoreet lorem, ac maximus quam suscipit eget.",
                "Sed vitae felis elementum, molestie sem in, facilisis ligula. Proin faucibus ligula eu metus molestie vestibulum. In hac habitasse platea dictumst. Sed enim diam, ornare ut tincidunt at, ultrices congue est. Nam sed ipsum ac arcu tristique luctus. Aenean varius felis est, non pretium augue efficitur id. Cras eget volutpat lorem.",
                "Nunc purus nulla, sodales quis fermentum eu, pharetra at nunc. Duis non turpis ornare, blandit purus at, tristique augue. Nullam id sagittis mauris. Suspendisse non felis sed eros auctor vestibulum. Nam ullamcorper ac ante nec porta. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Sed ac ligula sit amet risus sagittis condimentum. In hac habitasse platea dictumst. Curabitur leo enim, pellentesque nec eros dictum, consectetur semper lorem. Proin magna quam, iaculis a nibh ut, placerat blandit leo. Ut scelerisque vestibulum neque, quis condimentum turpis semper commodo. Aliquam quis venenatis quam. Donec tempus malesuada leo eget consequat. Phasellus volutpat lacus non nibh feugiat vulputate. Vivamus turpis mauris, congue eu risus sit amet, maximus gravida risus.",
                "Morbi vel rhoncus justo. Curabitur scelerisque nunc ac lorem malesuada, eu volutpat lacus vulputate. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Etiam at ex tortor. Nam vestibulum mattis lorem vel dignissim. Maecenas interdum eu mauris tempor aliquam. Cras id metus varius, tristique magna eu, auctor velit. Aenean egestas sed orci et commodo. Morbi in lacus in tellus ultricies fringilla sed eget nulla. Aenean venenatis, leo sit amet blandit convallis, turpis sapien lacinia neque, nec suscipit ex orci vitae turpis. In leo nulla, venenatis eget diam at, convallis consequat diam. In purus ante, fermentum eu arcu eu, congue venenatis eros. Duis euismod dictum ex, id posuere felis vehicula eu. Donec id porttitor diam, nec blandit justo. Proin ut condimentum diam.",
                "Vestibulum nisi eros, elementum et posuere non, lobortis et erat. Vestibulum in libero eget metus elementum dictum. Praesent scelerisque fringilla placerat. Suspendisse potenti. Maecenas nec sollicitudin augue, non vehicula purus. Vivamus at volutpat nisi, at suscipit est. Suspendisse consectetur justo id elit maximus, vel semper dui vehicula. Vivamus leo felis, suscipit rutrum rhoncus id, molestie in tortor. Vestibulum vehicula maximus arcu, nec euismod nunc tempus a. Nunc sed ligula vel nisi pharetra malesuada ac porta diam. Interdum et malesuada fames ac ante ipsum primis in faucibus. Praesent porta, nisl quis consequat suscipit, ipsum tellus dictum leo, a cursus orci felis sit amet enim. Aenean id ultricies diam.",
                "Duis vitae pharetra sem. Nulla egestas lectus nec nibh mollis volutpat. Nulla mattis odio tortor, non dapibus elit rhoncus vel. Duis fringilla vehicula nulla. Morbi a sagittis turpis, a tincidunt dui. Quisque eu ex nisi. Donec finibus nisi vel nunc volutpat, in feugiat massa consequat. Integer a nisl vel lectus varius varius. Pellentesque varius condimentum lacus eu elementum. Pellentesque rhoncus gravida erat hendrerit imperdiet. Ut mattis volutpat est.",
                "Praesent ornare luctus risus, eu facilisis lorem pulvinar sed. Praesent in sodales nibh, sed pretium arcu. Nulla ultricies finibus velit scelerisque cursus. Curabitur nec nibh sed velit molestie feugiat vel non nisi. Nulla facilisi. Etiam vitae imperdiet justo. Morbi tempus nisl ac rutrum feugiat. Nulla risus nisl, vehicula nec lectus id, iaculis tristique nisi. Vestibulum congue malesuada erat vitae malesuada. In porttitor tortor eget ipsum commodo mattis. Phasellus porta nunc in accumsan egestas. Integer bibendum, purus ut tristique volutpat, eros magna rhoncus nulla, id fringilla metus mauris nec augue. Nulla sagittis volutpat nisi, lacinia ultrices ex suscipit ut.",
                "Morbi bibendum tristique ipsum condimentum facilisis. Nulla tristique nunc id dui rutrum, eu gravida metus vestibulum. Nam malesuada nunc et mi feugiat, eget molestie lorem iaculis. Integer rhoncus odio nisl. Sed elementum mi eu magna egestas, et scelerisque lacus elementum. Nunc accumsan eleifend dui, sed molestie justo. Donec rutrum tincidunt nibh, sit amet consequat erat vulputate a. Proin scelerisque sapien quis vulputate fermentum. Praesent sit amet augue sed nulla commodo ultrices non nec orci. Proin porta efficitur urna, id suscipit ante pretium a. Praesent iaculis nulla tristique feugiat gravida. Phasellus tempor nunc sed ipsum blandit, convallis facilisis risus tincidunt. Nulla vulputate elit odio, nec pellentesque lorem semper vel. Duis vel nisl quis nibh cursus iaculis.",
                "Fusce volutpat magna ex, et pulvinar mi posuere in. Sed eu leo arcu. Donec arcu lacus, faucibus at cursus laoreet, pretium ut mauris. Nulla ut cursus mauris. Vestibulum consectetur ligula id urna consectetur ultricies. Suspendisse porttitor sem at rhoncus aliquam. Vestibulum et dolor libero. Interdum et malesuada fames ac ante ipsum primis in faucibus. Donec at leo eget nulla iaculis iaculis accumsan eget erat. Mauris quis urna nibh. Proin sodales suscipit nibh ultricies ultrices. Quisque semper nisl sed nibh placerat fermentum. Donec at ligula sed ipsum sagittis condimentum nec pulvinar dui. Quisque in eleifend orci. Quisque non fringilla lectus, varius pharetra est. Nam risus purus, efficitur quis est ut, imperdiet eleifend elit.",
                "Aenean vel ipsum interdum, laoreet mauris faucibus, pretium mauris. Donec laoreet porta velit et elementum. Duis facilisis odio eu quam aliquam egestas. Nullam nulla metus, interdum nec suscipit vitae, scelerisque id velit. Duis molestie id magna id varius. Integer volutpat sagittis erat ut fringilla. Nam vulputate, tortor ut luctus bibendum, ex mi accumsan turpis, quis consequat felis elit non nisi. Duis vel tellus eu enim feugiat ultricies. Morbi malesuada ligula aliquet, tincidunt elit eget, placerat eros. Aenean ipsum diam, lacinia eu tincidunt quis, gravida a velit. Integer vitae convallis purus. Aenean tortor elit, gravida vitae imperdiet at, faucibus vel urna. Pellentesque id mattis massa. Donec efficitur augue in mattis ornare. Pellentesque congue ex a lacus placerat, ac dignissim tellus viverra. Nam imperdiet nibh quis risus hendrerit volutpat.",
                "Integer posuere maximus mi, eget sollicitudin leo facilisis vitae. Fusce quis nisi malesuada, gravida dolor interdum, blandit arcu. Praesent faucibus mollis felis at finibus. Etiam sollicitudin semper eleifend. Aliquam nec tellus magna. Etiam vel magna imperdiet, commodo eros non, convallis enim. Integer pellentesque faucibus sapien non commodo. Cras iaculis lorem non bibendum cursus. Aenean euismod, lectus a aliquet semper, lacus lectus porttitor lacus, condimentum lacinia nulla nibh ut est. Aliquam lacinia eros condimentum, porttitor ipsum vel, aliquet libero. Morbi non leo vehicula, vehicula nibh eu, placerat orci. Pellentesque sagittis, nulla sed efficitur rutrum, risus arcu lacinia neque, eget volutpat magna libero eu orci.",
                "Pellentesque turpis mauris, egestas quis rhoncus in, volutpat in nisi. Morbi laoreet in ligula in ultricies. Proin sed justo euismod, tristique nibh at, facilisis nunc. Donec dapibus purus non malesuada eleifend. Aliquam non tortor accumsan magna laoreet pulvinar at ut nibh. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. In congue libero et tempus condimentum. Integer ut mi vitae velit placerat feugiat. Morbi rhoncus et neque et vestibulum. Proin ut quam a orci luctus porttitor quis nec nulla.",
                "Nulla et facilisis libero. Sed in eros eget dui volutpat scelerisque. Proin auctor quis lorem non aliquam. Quisque erat sem, aliquam sit amet neque eget, porta faucibus mauris. Maecenas porttitor, tellus vitae egestas commodo, ante urna blandit magna, at laoreet nunc dolor in lorem. Aliquam et erat cursus, scelerisque justo nec, volutpat nisi. Fusce at tortor non mi lacinia mollis. Sed elit erat, molestie a interdum vel, viverra ac urna. Pellentesque ac nisi quis sem vulputate molestie sit amet quis augue. Vestibulum ut ante sed augue aliquam venenatis. Sed finibus suscipit ex sed commodo. Nulla semper egestas lacus, eget aliquam dui pellentesque et. Duis quis arcu ipsum. Quisque laoreet ex nulla, quis volutpat lacus pretium non. Nullam sollicitudin diam lorem, vitae posuere nibh pharetra maximus.",
                "Donec non gravida eros. Donec ullamcorper dolor finibus eros elementum, at pellentesque lacus sagittis. Sed maximus nec elit vel placerat. Donec auctor varius orci, vel condimentum ex congue sit amet. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Etiam diam nisl, cursus et risus nec, varius fermentum erat. Cras ornare in sapien tincidunt aliquet. Nam interdum sapien ut elit pretium, in luctus nisi ornare. Aenean convallis auctor nibh eu faucibus.",
                "Praesent egestas odio vel euismod varius. Nulla sed nisi eget erat maximus ullamcorper ac a nibh. Duis ultrices, purus sed convallis suscipit, arcu elit eleifend nunc, quis dignissim arcu odio et massa. Maecenas consectetur urna augue, semper porta ante semper sed. Praesent porttitor augue at nulla sollicitudin, in hendrerit ex laoreet. Phasellus pretium lectus vitae laoreet interdum. Nunc ac sollicitudin leo. Vivamus vulputate tincidunt ipsum ut porta. Nullam pulvinar ante tortor, in blandit diam interdum in. In eget mi laoreet, tristique magna vel, posuere enim. Maecenas suscipit arcu nec scelerisque bibendum.",
            };
            return paragraphs[rnd.Next(0, paragraphs.Count)];
        }


    }
}
