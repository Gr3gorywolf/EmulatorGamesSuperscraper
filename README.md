# EmulatorGamesSuperscraper
Emulator.games webscraper+ .netcore extractor application that allows you to extract the info from emulator.games and export it to json that can be used on third party applications
and the core of this app is used on <a href='https://github.com/Gr3gorywolf/NeonRom3r'>NeonRomer</a> the core is superscrapper.cs that makes all the request etc
the program.cs is the extractor that is also used as a demo to explain how to use the scraping core
# Usage
## Include the library to your proyect
You can download the dll that is currently on the dll folder or also download it clicking <a href="https://github.com/Gr3gorywolf/EmulatorGamesSuperscraper/raw/master/dll/emulatorgamessuperscraper.dll">Here</a>
Once you have the dll just import to your proyect
### Note
This library have the following dependeces
.net core 2.0/n
Html agility pack 1.8.7/n
For the console app/n
TextCopy 1.5.0/n
Newtonsoft.Json 11.0.2/n
## Import of the library
```c#
/////Include the library on the top of the code writting
using emulatorgamessuperscrapper
///////////////////////////////////////////////////////
///the library contains 2 primary classes that are
///////the scrapper
/*is used to scrap info from the page and have only methods for it */
emulatorgamessuperscrapper.superscraper
///////the modals
/*is used to modelize the data for better organization and is used for the superscraper methods for better output*/
emulatorgamessuperscrapper.Models




```

## The superscraper class

```c#
superscraper scrap = new superscraper();
```
### Get info from a specific rom
```c#
/*getrominfo
is an async task that returns you a data from the specific rom
as an models.Rominfo and also you need to specify the link of the rom as a parameter

*/

scrap.getrominfo(<rom link>)
// and also is awaitable
var model = await scrap.getrominfo(<rom link>);
//or you can use it synchronously
var model = scrap.getrominfo(<rom link>).result;
```
### Get info of roms from a specific console
```c#
/*getwebdata
is an async task that returns you a array of data from an specific console
as a list of  models.romsinfos and also you need to 
specify the console that could be the follows
 "gameboy-advance",
 "super-nintendo",
 "nintendo-64", 
 "nintendo",
 "playstation",
 "gameboy-color", 
 "sega-genesis", 
 "gameboy",
 "dreamcast"
 
 And also the number of pages as second parameter(if you choose a lot of pages
 it will take more time to give the info)
 and as third parameter is an boolean that 
 if is true will get the link of hd portraits but if it 
 is false it will get the low res portraits
 
*/
var Data =  scrap.getwebdata(<console name>,<Number of pages>,<portraits quality>);
///and also is awaitable
var Data = await scrap.getwebdata(<console name>,<Number of pages>,<portraits quality>);
//or you can use it synchronously
var Data = scrap.getwebdata(<console name>,<Number of pages>,<portraits quality>).result;
```
### Get direct download link of a rom once you have extracted their information
```c#
/*getdownloadlink
is an async task that allow you to get the direct download link as a string 
for use this method you only need to have the id of the rom that you can get extracting
the info of that rom
*/
  var link =  scrap.getdownloadlink(<rom id>)
  //and also is awaitable
  var link =await  scrap.getdownloadlink(<rom id>);
  //or you can use it synchronously
  var link =await  scrap.getdownloadlink(<rom id>).result;
```

