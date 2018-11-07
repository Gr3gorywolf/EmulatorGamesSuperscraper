# EmulatorGamesSuperscraper
Emulator.games webscraper+ .netcore extractor application that allows you to extract the info from emulator.games and export it to json that can be used on third party applications
and the core of this app is used on <a href='https://github.com/Gr3gorywolf/NeonRom3r'>NeonRomer</a> the core is superscrapper.cs that makes all the request etc
the program.cs is the extractor that is also used as a demo to explain how to use the scraping core
# Usage
### Include the library and the usage of the classes in the library
```c#
/////unclude the library on the top of the code writting
using emulatorgamessuperscrapper
///////////////////////////////////////////////////////
///the library contains 2 primary classes that are
///////the scrapper
/*is used to scrap info from the page and have only methods for it */
emulatorgamessuperscrapper.superscraper
///////the modals
/*is used to modelize the data for better organization*/
emulatorgamessuperscrapper.Models




```

### usage of the superscraper class
```
superscraper scrap=new superscraper();
/*getrominfo
is an async task that returns you a data from the specific rom
as an models.Rominfo and also you need to specify the link of the rom as a parameter

*/
scrap.getrominfo(<rom link>)
// and also is awaitable
var model= await scrap.getrominfo(<rom link>);
//or you can use it synchronously
var model= scrap.getrominfo(<rom link>).result;
/*getwebdata
is an async task that returns you a array of data from an specific console
as a list of  models.romsinfos and also you need to specify the console that could be the follows
 "gameboy-advance",
 "super-nintendo",
 "nintendo-64", 
 "nintendo",
 "playstation",
 "gameboy-color", 
 "sega-genesis", 
 "gameboy",
 "dreamcast"
 
 And also the number of pages as second parameter(if you choose a lot of pages it will take more time to give the info)
 and as third parameter is an boolean that if is true will get the link of hd portraits but if it is false it will get the low res portraits
 
*/
var Data =  escrapeador.getwebdata("gameboy-advance",2,false);
///and also is awaitable
var Data = await escrapeador.getwebdata("gameboy-advance",2,false);
//or you can use it synchronously
var Data =escrapeador.getwebdata("gameboy-advance",2,false).result;
```

