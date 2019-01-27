using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace AlmostAreBugs_JSON_Generator {
    class Csv_Reader {
        public readonly string[] fileList = {"asdf"};
        public readonly string[][][] dataRead;
        readonly StreamReader streamReader;
        public Csv_Reader() {

            string path = System.IO.Directory.GetCurrentDirectory();
            DirectoryInfo directoryInfo = new DirectoryInfo( path );

            var validFiles = from file in directoryInfo?.GetFiles()
                             where file.Extension == ".csv" && file.Name.Contains( "Data" )
                             select file;

            if( validFiles == null )
                throw new FileNotFoundException();

            dataRead = new string[ validFiles.Count() ][][];
            foreach( var file in validFiles ) {
                for( int i = 0; i < fileList.Length; i++ ) {
                    if( file.Name.Contains( fileList[ i ] ) ) {
                        streamReader = new StreamReader( file.FullName, Encoding.Unicode ); //일까요.
                        ReadDataInStreamReader( streamReader, out dataRead[ i ] );
                    }
                }
            }
        }
        public string[][] Read(string str ) {
            for( int i = 0; i < fileList.Length; i++ )
                if( str == fileList[ i ] )
                    return dataRead[ i ];
            System.Console.WriteLine( "유요하지 않은 데이터 이름입니다: {1}", str );
            throw new ArgumentException();
        }
        private void ReadDataInStreamReader( StreamReader streamReader, out string[][] dataOutput ) {
            
            int argnum;
            if( streamReader.EndOfStream || ( argnum = streamReader.ReadLine().Split( ',' ).Length ) == 0 ) 
                throw new FormatException();
            dataOutput = new string[ argnum ][];
            List<string> templist = new List<string>();

            while( !streamReader.EndOfStream )
                templist.Add( streamReader.ReadLine() );

            for(int i=0; i< argnum; i++ )
                dataOutput[ i ] = new string[ templist.Count ];

            for(int i=0; i<templist.Count; i++ ) {
                if(templist[i].Length!= argnum ) {
                    Console.WriteLine( "{1} 스트림의 {2}번째 줄에서 형식오류가 발생했습니다.", streamReader.ToString(),i );
                    throw new FormatException();
                }
                for(int j=0; j< argnum; j++ ) {
                    dataOutput[ j ][ i ] = templist[ i ].Split( ',' )[ j ];
                }
            }
        }
    }
}
