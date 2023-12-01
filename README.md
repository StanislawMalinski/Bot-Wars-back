# Bot-Wars-back
1. Pobierz .Net 8 (runtime i SDK) oraz sql server management studio w swojej ulubionej wersji
2. stwórz baze danych o nazwie Games w sql server management studio
3. Ustaw połącznie do bazy danych w appsetings.json - "DefaultConnection" (zmieniasz 'Server' tylko)
4. Wykonuejsz komendę w konsoli visual studio Update-Database do postawienia bazy ( jak nie zadziała to usuń wszystkie migracje z folderu migracji i wpisz komendę Add-Migration a potem Update-Database)
5. Uruchominie powinno odpalic w przeglądarce swagger'a - (takie coś do testowania)
6. Baza zapisuje dowolne pliki do 64kB;
