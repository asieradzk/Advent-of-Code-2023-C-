﻿// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


//var _5Test = "seeds: 79 14 55 13\r\n\r\nseed-to-soil map:\r\n50 98 2\r\n52 50 48\r\n\r\nsoil-to-fertilizer map:\r\n0 15 37\r\n37 52 2\r\n39 0 15\r\n\r\nfertilizer-to-water map:\r\n49 53 8\r\n0 11 42\r\n42 0 7\r\n57 7 4\r\n\r\nwater-to-light map:\r\n88 18 7\r\n18 25 70\r\n\r\nlight-to-temperature map:\r\n45 77 23\r\n81 45 19\r\n68 64 13\r\n\r\ntemperature-to-humidity map:\r\n0 69 1\r\n1 0 69\r\n\r\nhumidity-to-location map:\r\n60 56 37\r\n56 93 4";

//var _5Input = "seeds: 1972667147 405592018 1450194064 27782252 348350443 61862174 3911195009 181169206 626861593 138786487 2886966111 275299008 825403564 478003391 514585599 6102091 2526020300 15491453 3211013652 546191739\r\n\r\nseed-to-soil map:\r\n325190047 421798005 78544109\r\n4034765382 1473940091 137996533\r\n403734156 658668780 288666603\r\n2574766003 2624114227 17352982\r\n1931650757 2203381572 98211987\r\n4263596455 2843660329 31370841\r\n1614547845 2641467209 55121215\r\n3441604278 2032673361 170708211\r\n692400759 563703672 94965108\r\n2992851755 3824700930 114550818\r\n3957953582 2540115966 24844899\r\n0 500342114 59804107\r\n4172761915 2577017231 47096996\r\n2029862744 3548344768 52256082\r\n2620304610 2875031170 99567251\r\n3982798481 3939251748 51966901\r\n2325101894 1616388089 203150170\r\n3612312489 1611936624 4451465\r\n787365867 0 77461445\r\n1341614907 4162572181 132395115\r\n2542801978 3468402968 31964025\r\n223387052 947335383 59156225\r\n2297805420 1819538259 27296474\r\n2082118826 3991218649 171353532\r\n3374211778 3040047171 67392500\r\n2592118985 1341614907 28185625\r\n2253472358 2495782904 44333062\r\n4219858911 3780963386 43737544\r\n59804107 560146221 3557451\r\n3107402573 3600600850 64973479\r\n1669669060 3115086052 235712356\r\n2719871861 1369800532 104139559\r\n3172376052 2301593559 194189345\r\n2980795389 2564960865 12056366\r\n1905381416 3350798408 26269341\r\n63361558 261772511 160025494\r\n2824011420 3377067749 91335219\r\n3366565397 3107439671 7646381\r\n282543277 77461445 42646770\r\n3713793353 2696588424 147071905\r\n2528252064 1877585624 14549914\r\n3927202691 1846834733 30750891\r\n3665815578 3500366993 47977775\r\n1474010022 1892135538 140537823\r\n3616763954 3665574329 49051624\r\n2915346639 2974598421 65448750\r\n864827312 120108215 141664296\r\n3860865258 3714625953 66337433\r\n\r\nsoil-to-fertilizer map:\r\n3835605444 4098164436 1662218\r\n682286299 0 63480553\r\n396476124 2072802434 285810175\r\n1644893571 655614677 162631098\r\n3625179075 4099826654 40627721\r\n1431211762 1859120625 213681809\r\n2853687843 4140454375 103601386\r\n1390165578 2358612609 41046184\r\n2405285827 3959200676 138963760\r\n900243562 1478767251 170015757\r\n3727732722 3084190064 107872722\r\n893948759 1084467374 6294803\r\n210337617 818245775 186138507\r\n1807524669 63480553 592134124\r\n825849944 1090762177 68098815\r\n3837267662 3192062786 457699634\r\n0 1648783008 210337617\r\n3665806796 2405285827 61925926\r\n1070259319 1158860992 319906259\r\n745766852 1004384282 80083092\r\n2957289229 4244055761 50911535\r\n2544249587 3649762420 309438256\r\n3008200764 2467211753 616978311\r\n\r\nfertilizer-to-water map:\r\n1169336944 3024036226 46676171\r\n1263157944 1445876546 148868263\r\n2080390054 2683279801 65621604\r\n949040795 1266013203 61140343\r\n2146011658 2793621589 110265098\r\n525510412 2618124082 65155719\r\n2977122301 3122211455 179751286\r\n3885825304 713560214 409141992\r\n1081017627 4124851079 88319317\r\n590666131 2039951828 38597255\r\n3596454634 1981772613 58179215\r\n2256276756 2078549083 315165891\r\n1639631621 1122702206 143310997\r\n2858399301 1327153546 118723000\r\n3654633849 3301962741 72039868\r\n749412925 3549292249 161128796\r\n1412026207 541983534 151190298\r\n1054901322 4268850991 26116305\r\n1829368778 3374002609 175289640\r\n1216013115 2570979253 47144829\r\n2806900243 3070712397 51499058\r\n1563216505 3760425124 76415116\r\n1782942618 3710421045 29953038\r\n629263386 2903886687 120149539\r\n2060339013 3740374083 20051041\r\n3156873587 3836840240 52553243\r\n2693038006 4010988842 113862237\r\n3726673717 2411827666 159151587\r\n2004658418 4213170396 55680595\r\n1812895656 525510412 16473122\r\n1010181138 2748901405 44720184\r\n910541721 693173832 20386382\r\n930928103 2393714974 18112692\r\n3209426830 1594744809 387027804\r\n2571442647 3889393483 121595359\r\n\r\nwater-to-light map:\r\n555269773 2142838063 230952411\r\n2879443939 2889030006 80512763\r\n192641991 2686257040 106620606\r\n786222184 967781117 56662479\r\n3467110983 4162792381 132174915\r\n1955778230 0 505352386\r\n2461130616 1138855522 99691153\r\n1436321461 1403321335 440861327\r\n2625559119 1024443596 104825859\r\n1255310011 2792877646 96152360\r\n1877182788 697994377 78595442\r\n3732975066 3467110983 374194949\r\n842884663 2373790474 56459390\r\n299262597 2430249864 256007176\r\n3599285898 3841305932 133689168\r\n4107170015 3974995100 187797281\r\n0 505352386 192641991\r\n1090535351 1238546675 164774660\r\n2959956702 1129269455 9586067\r\n1351462371 1844182662 84859090\r\n2730384978 1929041752 149058961\r\n899344053 776589819 191191298\r\n2560821769 2078100713 64737350\r\n\r\nlight-to-temperature map:\r\n4103141199 3912772142 105835099\r\n1994281004 833968687 112844016\r\n4208976298 1124841590 85990998\r\n3756966983 4018607241 111390720\r\n3868357703 1907239336 234783496\r\n2368591640 1210832588 293667703\r\n882426579 2320467318 1111854425\r\n3064998388 717457244 33489309\r\n3578938096 946812703 178028887\r\n2107125020 750946553 83022134\r\n3157760738 3432321743 421177358\r\n717457244 4129997961 164969335\r\n2190147154 2142022832 178444486\r\n3098487697 3853499101 59273041\r\n2662259343 1504500291 402739045\r\n\r\ntemperature-to-humidity map:\r\n0 1820412620 129662806\r\n613828383 2855382935 55943394\r\n4004726464 3519717349 290240832\r\n769767991 99996214 1720416406\r\n3126066249 3043992795 475724554\r\n2490184397 2434241003 421141932\r\n129662806 1950075426 484165577\r\n689805485 0 79962506\r\n669771777 79962506 20033708\r\n3601790803 3809958181 402935661\r\n3043992795 4212893842 82073454\r\n\r\nhumidity-to-location map:\r\n1305211417 3371927062 89487200\r\n947159122 0 358052295\r\n324330151 2021970861 8067408\r\n332397559 654359706 174171341\r\n506568900 3311893445 60033617\r\n11065691 828531047 45586147\r\n3556729147 369117986 26689998\r\n3583419145 395807984 258551722\r\n2356886904 984938593 400606547\r\n1394698617 874117194 110821399\r\n566602517 3946624826 164852367\r\n2998901322 2301963630 256425441\r\n0 358052295 11065691\r\n3331964991 3087129289 34908052\r\n1505520016 2106676497 195287133\r\n56651838 2819450976 267678313\r\n3366873043 3293025783 18867662\r\n3385740705 3122037341 170988442\r\n2281795782 2799796942 19654034\r\n2301449816 1966533773 55437088\r\n3992934054 3677118500 118543139\r\n3255326763 2030038269 76638228\r\n3849868384 3803559156 143065670\r\n2757493451 2558389071 241407871\r\n3841970867 3795661639 7897517\r\n1700807149 1385545140 580988633\r\n731454884 3461414262 215704238";


//solution is 52510809
//for when I want to solve this without brute-force

//var output = _8.mySolution3(_8Input);
//Console.WriteLine(output);


