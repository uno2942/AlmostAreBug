using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace AlmostAreBugs_JSON_Generator {
    class Csv_Reader {
        public List<List<List<string>>> listlistlist;
        public List<string> fileList;
        public Csv_Reader() {
            listlistlist = new List<List<List<string>>>();
            string path = System.IO.Directory.GetCurrentDirectory();
            DirectoryInfo directoryInfo = new DirectoryInfo( path );
            fileList = new List<string>();
            int i = 0;
            foreach( var file in directoryInfo.GetFiles() ) {
                if( file.Extension == ".csv" ) {
                    StreamReader streamReader = new StreamReader( file.FullName, Encoding.UTF8 ); //일까요.
                    listlistlist.Add( new List<List<string>>() );
                    fileList.Add( file.Name.Split('.')[0] );
                    while( !streamReader.EndOfStream ) {
                        listlistlist[ i ].Add( new List<string>( streamReader.ReadLine().Split( ',' ) ) );
                    }
                    i++;
                }
            }
        }
    }
}