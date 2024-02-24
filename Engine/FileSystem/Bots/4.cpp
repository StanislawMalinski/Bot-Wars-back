#include <vector>
#include <iostream>
#include <string>
#include <unordered_set>
#include <queue>
#include <map>
#include <set>
#include <stack>
#include <cstring>
#include <math.h>
#include <algorithm>
#include <unordered_map>
using namespace std;



int main(){
	int res;
    int me = 0,enemy = 0;
    FILE * plik;
    plik = fopen( "nazwa.txt", "wt" );
    fprintf( plik, "Tutaj wpisujemy sobie" );
    fprintf( plik, "Dane tekstowe" );
    fprintf( plik, "Jakie tylko chcesz" );
   
    fclose( plik );
    while( me < 3 && enemy < 3){
        cin >> res;
        cout << 1 << endl;
        
        if(res == -1){
            enemy++;
        }else if (res == 1){
            me++;
        }
    }
    
}
