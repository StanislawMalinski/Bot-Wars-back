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
    srand (time(NULL));
    while( me < 3 && enemy < 3){
        cin >> res;
        cout << rand()%3 << endl;
        
        if(res == -1){
            enemy++;
        }else if (res == 1){
            me++;
        }
    }
    
}
