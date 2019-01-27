using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace AlmostAreBugs_JSON_Generator {
    class Csv_Reader {
        public Csv_Reader() {
            string path = System.IO.Directory.GetCurrentDirectory();
            DirectoryInfo directoryInfo = new DirectoryInfo( path );
            foreach( var file in directoryInfo.GetFiles() ) {
                if( file.Extension == ".csv" ) {

                    StreamReader streamReader = new StreamReader( file.FullName, Encoding.Unicode ); //일까요.
                    while( streamReader.EndOfStream ) {

                    }
                    //뭔갈 함.
                }
            }
        }
        
    }
}
