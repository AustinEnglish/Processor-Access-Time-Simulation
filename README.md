# About / Synopsis

* Consider a system, with k processing elements and m memory modules interconected by a network capable of pairing any processor to any
memory. The goal of this project is to simulate this type of architecture with the following rules:
* 1. Time is divided into equal memory cycles (in this case a for loop for simulation purposes)
* 2. Processing elements request access to memory modules at the beginning of a memory cycle and follow a priority scheme(in this case having a priority queue)
* 4. Processing elements are connected to random memory moduels either based of a unifrom distribution or a gaussian distribution.
* 5. Processing elements are connected to their requested memory module if the module is available, if not they must wait to connect to the SAME memory module the nect clock cycle.
* 6. At the beginning of the next cycle, all processing elements that were granted access generate a new request while those who waited retry access to the previously denied memory module.
* 7. Each cycle ends when compared with last runs average with currebt runs average, if less that .02% terminate
* 8. The simulation ends at once all 0-2047 memory models run with a fixed certain amount of processors
## Screenshots
![Design](https://github.com/AustinEnglish/Processor-Access-Time-Simulation/blob/master/architechture.jpg?raw=true "Title")







