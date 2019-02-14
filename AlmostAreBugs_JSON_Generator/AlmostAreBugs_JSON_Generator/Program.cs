using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Newtonsoft.Json;
using System.IO;
namespace AlmostAreBugs_JSON_Generator {
    class Program {
        static void Main( string[] args ) {

            Csv_Reader csvReader = new Csv_Reader();

            DataSet dataSet;
            int j = 0;

            
            string[] temp2 = System.IO.Directory.GetCurrentDirectory().Split( '\\' );
            List<string> temp3 = new List<string>( ( temp2.Reverse().Skip( 4 ) ).Reverse() );
            temp3.Add( "\\AlmostAreBugs\\Assets\\StreamingAssets" );
            string writingFilePath = String.Join( @"\", temp3 );
            foreach( List<List<string>> Script in csvReader.listlistlist ) {
                dataSet = new DataSet( csvReader.fileList[j] );
                dataSet.Namespace = "NetFrameWork";
                DataTable table = new DataTable();
                DataColumn[] Columns = new DataColumn[ Script[ 0 ].Count ];
                for( int i = 0; i < Script[ 0 ].Count; i++ ) {
                    Columns[ i ] = new DataColumn( Script[ 0 ][ i ] );
                    table.Columns.Add( Columns[ i ] );
                }
                dataSet.Tables.Add( table );
                int catnum = Script[ 0 ].Count;
                Script.RemoveAt( 0 );

                DataRow dataRow;
                foreach( List<string> aLine in Script ) {
                    dataRow = table.NewRow();
                    for( int i = 0; i < catnum; i++ ) {
                        dataRow[ i ] = aLine[ i ];
                    }
                    table.Rows.Add( dataRow );
                }


                dataSet.AcceptChanges();

                string json = JsonConvert.SerializeObject( dataSet, Formatting.Indented );
                File.WriteAllText( writingFilePath + '\\' + csvReader.fileList[ j ] + ".json", json );
                j++;
            }
        }
    }
}