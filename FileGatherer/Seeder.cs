﻿using Microsoft.AspNetCore.Http.HttpResults;
using System.Net.Http;
using System.Text;

namespace FileGatherer
{
    public class Seeder
    {
        private readonly GathererService _gatherer;
        private string placeholder0 = "placeholder 0";
        private string bot1Bj_cpp = "#include <iostream>\r\n#include <vector>\r\n#include <string>\r\nusing namespace std;\r\n\r\nstruct Card {\r\n    string suit;\r\n    string faceValue;\r\n};\r\n\r\nbool isValidCard(const string& faceValue, const string& suit) {\r\n    string acceptedFaceValues[] = {\"2\", \"3\", \"4\", \"5\", \"6\", \"7\", \"8\", \"9\", \"10\", \"Jack\", \"Queen\", \"King\", \"Ace\"};\r\n    string acceptedSuits[] = {\"Hearts\", \"Diamonds\", \"Clubs\", \"Spades\"};\r\n\r\n    bool isValidFaceValue = false;\r\n    for (const string& acceptedValue : acceptedFaceValues) {\r\n        if (faceValue == acceptedValue) {\r\n            isValidFaceValue = true;\r\n            break;\r\n        }\r\n    }\r\n\r\n    bool isValidSuit = false;\r\n    for (const string& acceptedSuit : acceptedSuits) {\r\n        if (suit == acceptedSuit) {\r\n            isValidSuit = true;\r\n            break;\r\n        }\r\n    }\r\n\r\n    return isValidFaceValue && isValidSuit;\r\n}\r\n\r\nint main() {\r\n    string input;\r\n    \r\n    vector<long> cossr(1000000);\r\n    cossr[12] = 232;\r\n    vector<Card> playerHand;\r\n    vector<Card> dealerHand;\r\n\r\n    while (true) {\r\n        getline(cin, input);\r\n        playerHand = vector<Card>();\r\n        dealerHand = vector<Card>();\r\n        size_t dealerIndex = input.find(\"d:\");\r\n        string playerData = input.substr(0, dealerIndex);\r\n        string dealerData = input.substr(dealerIndex + 2);\r\n\r\n        size_t pos = 0;\r\n        string delimiter = \" \";\r\n        while ((pos = playerData.find(delimiter)) != string::npos) {\r\n            string cardStr = playerData.substr(0, pos);\r\n            size_t underscorePos = cardStr.find(\"_\");\r\n            string faceValue = cardStr.substr(0, underscorePos);\r\n            string suit = cardStr.substr(underscorePos + 1);\r\n            if (isValidCard(faceValue, suit)) {\r\n                playerHand.push_back({suit, faceValue});\r\n            }\r\n            playerData.erase(0, pos + delimiter.length());\r\n        }\r\n\r\n        pos = 0;\r\n        while ((pos = dealerData.find(delimiter)) != string::npos) {\r\n            string cardStr = dealerData.substr(0, pos);\r\n            size_t underscorePos = cardStr.find(\"_\");\r\n            string faceValue = cardStr.substr(0, underscorePos);\r\n            string suit = cardStr.substr(underscorePos + 1);\r\n            if (isValidCard(faceValue, suit)) {\r\n                dealerHand.push_back({suit, faceValue});\r\n            }\r\n            dealerData.erase(0, pos + delimiter.length());\r\n        }\r\n\r\n        size_t underscorePos = dealerData.find(\"_\");\r\n        string faceValue = dealerData.substr(0, underscorePos);\r\n        string suit = dealerData.substr(underscorePos + 1);\r\n        if (isValidCard(faceValue, suit)) {\r\n            dealerHand.push_back({suit, faceValue});\r\n        }\r\n        cout << \"hit\" << endl;\r\n    }\r\n\r\n    return 0;\r\n}";
        private string bot2Bj_cpp = "#include <iostream>\r\n#include <vector>\r\n#include <string>\r\nusing namespace std;\r\n\r\nstruct Card {\r\n    string suit;\r\n    string faceValue;\r\n};\r\n\r\nint countPoints(const vector<Card>& hand) {\r\n    int points = 0;\r\n    int numAces = 0;\r\n\r\n    for (const auto& card : hand) {\r\n        if (card.faceValue == \"Jack\" || card.faceValue == \"Queen\" || card.faceValue == \"King\") {\r\n            points += 10;\r\n        } else if (card.faceValue == \"Ace\") {\r\n            numAces++;\r\n        } else {\r\n            try {\r\n                int value = stoi(card.faceValue);\r\n                points += value;\r\n            } catch (const invalid_argument& e) {\r\n                cerr << \"Nieprawidłowa wartość karty: \" << card.faceValue << endl;\\\r\n            }\r\n        }\r\n    }\r\n    while (points > 21 && numAces > 0) {\r\n        points -= 10;\r\n        numAces--;\r\n    }\r\n    return points;\r\n}\r\n\r\nbool isValidCard(const string& faceValue, const string& suit) {\r\n    string acceptedFaceValues[] = {\"2\", \"3\", \"4\", \"5\", \"6\", \"7\", \"8\", \"9\", \"10\", \"Jack\", \"Queen\", \"King\", \"Ace\"};\r\n    string acceptedSuits[] = {\"Hearts\", \"Diamonds\", \"Clubs\", \"Spades\"};\r\n\r\n    bool isValidFaceValue = false;\r\n    for (const string& acceptedValue : acceptedFaceValues) {\r\n        if (faceValue == acceptedValue) {\r\n            isValidFaceValue = true;\r\n            break;\r\n        }\r\n    }\r\n\r\n    bool isValidSuit = false;\r\n    for (const string& acceptedSuit : acceptedSuits) {\r\n        if (suit == acceptedSuit) {\r\n            isValidSuit = true;\r\n            break;\r\n        }\r\n    }\r\n\r\n    return isValidFaceValue && isValidSuit;\r\n}\r\n\r\nint main() {\r\n    string input;\r\n\r\n    vector<Card> playerHand;\r\n    vector<Card> dealerHand;\r\n\r\n    while (true) {\r\n        getline(cin, input);\r\n        playerHand = vector<Card>();\r\n        dealerHand = vector<Card>();\r\n        size_t dealerIndex = input.find(\"d:\");\r\n        string playerData = input.substr(0, dealerIndex);\r\n        string dealerData = input.substr(dealerIndex + 2);\r\n\r\n        size_t pos = 0;\r\n        string delimiter = \" \";\r\n        while ((pos = playerData.find(delimiter)) != string::npos) {\r\n            string cardStr = playerData.substr(0, pos);\r\n            size_t underscorePos = cardStr.find(\"_\");\r\n            string faceValue = cardStr.substr(0, underscorePos);\r\n            string suit = cardStr.substr(underscorePos + 1);\r\n            if (isValidCard(faceValue, suit)) {\r\n                playerHand.push_back({suit, faceValue});\r\n            }\r\n            playerData.erase(0, pos + delimiter.length());\r\n        }\r\n\r\n        pos = 0;\r\n        while ((pos = dealerData.find(delimiter)) != string::npos) {\r\n            string cardStr = dealerData.substr(0, pos);\r\n            size_t underscorePos = cardStr.find(\"_\");\r\n            string faceValue = cardStr.substr(0, underscorePos);\r\n            string suit = cardStr.substr(underscorePos + 1);\r\n            if (isValidCard(faceValue, suit)) {\r\n                dealerHand.push_back({suit, faceValue});\r\n            }\r\n            dealerData.erase(0, pos + delimiter.length());\r\n        }\r\n\r\n        size_t underscorePos = dealerData.find(\"_\");\r\n        string faceValue = dealerData.substr(0, underscorePos);\r\n        string suit = dealerData.substr(underscorePos + 1);\r\n        if (isValidCard(faceValue, suit)) {\r\n            dealerHand.push_back({suit, faceValue});\r\n        }\r\n        if (countPoints(playerHand)>16)\r\n            cout << \"stand\" << endl;\r\n        else\r\n            cout << \"hit\" << endl;\r\n    }\r\n\r\n    return 0;\r\n}\r\n\r\n";
        private string bot3Bj_cpp = "##include <iostream>\r\n#include <vector>\r\n#include <string>\r\nusing namespace std;\r\n\r\nstruct Card {\r\n    string suit;\r\n    string faceValue;\r\n};\r\n\r\nint countPoints(const vector<Card>& hand) {\r\n    int points = 0;\r\n    int numAces = 0;\r\n\r\n    for (const auto& card : hand) {\r\n        if (card.faceValue == \"Jack\" || card.faceValue == \"Queen\" || card.faceValue == \"King\") {\r\n            points += 10;\r\n        } else if (card.faceValue == \"Ace\") {\r\n            numAces++;\r\n            points += 11;\r\n        } else {\r\n            try {\r\n                int value = stoi(card.faceValue);\r\n                points += value;\r\n            } catch (const invalid_argument& e) {\r\n                cerr << \"Nieprawidłowa wartość karty: \" << card.faceValue << endl;\\\r\n            }\r\n        }\r\n    }\r\n    while (points > 21 && numAces > 0) {\r\n        points -= 10;\r\n        numAces--;\r\n    }\r\n    return points;\r\n}\r\n\r\nbool isValidCard(const string& faceValue, const string& suit) {\r\n    string acceptedFaceValues[] = {\"2\", \"3\", \"4\", \"5\", \"6\", \"7\", \"8\", \"9\", \"10\", \"Jack\", \"Queen\", \"King\", \"Ace\"};\r\n    string acceptedSuits[] = {\"Hearts\", \"Diamonds\", \"Clubs\", \"Spades\"};\r\n\r\n    bool isValidFaceValue = false;\r\n    for (const string& acceptedValue : acceptedFaceValues) {\r\n        if (faceValue == acceptedValue) {\r\n            isValidFaceValue = true;\r\n            break;\r\n        }\r\n    }\r\n\r\n    bool isValidSuit = false;\r\n    for (const string& acceptedSuit : acceptedSuits) {\r\n        if (suit == acceptedSuit) {\r\n            isValidSuit = true;\r\n            break;\r\n        }\r\n    }\r\n\r\n    return isValidFaceValue && isValidSuit;\r\n}\r\n\r\nint main() {\r\n    string input;\r\n\r\n    vector<Card> playerHand;\r\n    vector<Card> dealerHand;\r\n\r\n    while (true) {\r\n        getline(cin, input);\r\n        playerHand = vector<Card>();\r\n        dealerHand = vector<Card>();\r\n        size_t dealerIndex = input.find(\"d:\");\r\n        string playerData = input.substr(0, dealerIndex);\r\n        string dealerData = input.substr(dealerIndex + 2);\r\n\r\n        size_t pos = 0;\r\n        string delimiter = \" \";\r\n        while ((pos = playerData.find(delimiter)) != string::npos) {\r\n            string cardStr = playerData.substr(0, pos);\r\n            size_t underscorePos = cardStr.find(\"_\");\r\n            string faceValue = cardStr.substr(0, underscorePos);\r\n            string suit = cardStr.substr(underscorePos + 1);\r\n            if (isValidCard(faceValue, suit)) {\r\n                playerHand.push_back({suit, faceValue});\r\n            }\r\n            playerData.erase(0, pos + delimiter.length());\r\n        }\r\n\r\n        pos = 0;\r\n        while ((pos = dealerData.find(delimiter)) != string::npos) {\r\n            string cardStr = dealerData.substr(0, pos);\r\n            size_t underscorePos = cardStr.find(\"_\");\r\n            string faceValue = cardStr.substr(0, underscorePos);\r\n            string suit = cardStr.substr(underscorePos + 1);\r\n            if (isValidCard(faceValue, suit)) {\r\n                dealerHand.push_back({suit, faceValue});\r\n            }\r\n            dealerData.erase(0, pos + delimiter.length());\r\n        }\r\n\r\n        size_t underscorePos = dealerData.find(\"_\");\r\n        string faceValue = dealerData.substr(0, underscorePos);\r\n        string suit = dealerData.substr(underscorePos + 1);\r\n        if (isValidCard(faceValue, suit)) {\r\n            dealerHand.push_back({suit, faceValue});\r\n        }\r\n        if (countPoints(playerHand)>18 && playerHand.size()==2)\r\n            cout<<\"double\" << endl;\r\n        else\r\n        {\r\n            if(countPoints(dealerHand) == 11)\r\n            cout<<\"insurance\" << endl;\r\n                \r\n            else{\r\n                if(countPoints(playerHand)<18)\r\n                cout<<\"hit\" << endl;\r\n                else cout<<\"stand\" << endl;\r\n                }\r\n        }\r\n    }\r\n\r\n    return 0;\r\n}";
        private string black_jack_cpp = "#include <iostream>\r\n#include <vector>\r\n#include <ctime>\r\n#include <cstdlib>\r\n#include <string>\r\n\r\nusing namespace std;\r\n\r\nstruct Card {\r\n    string suit;\r\n    string faceValue;\r\n};\r\n\r\nclass Deck {\r\nprivate:\r\n    vector<Card> cards;\r\n\r\npublic:\r\n    Deck() {\r\n        string suits[] = {\"Hearts\", \"Diamonds\", \"Clubs\", \"Spades\"};\r\n        string faceValues[] = {\"2\", \"3\", \"4\", \"5\", \"6\", \"7\", \"8\", \"9\", \"10\", \"Jack\", \"Queen\", \"King\", \"Ace\"};\r\n\r\n        for (const auto &suit : suits) {\r\n            for (const auto &faceValue : faceValues) {\r\n                cards.push_back({suit, faceValue});\r\n            }\r\n        }\r\n    }\r\n\r\n    void shuffle() {\r\n        srand(time(nullptr));\r\n        for (int i = 0; i < cards.size(); ++i) {\r\n            int randomIndex = rand() % cards.size();\r\n            swap(cards[i], cards[randomIndex]);\r\n        }\r\n    }\r\n\r\n    void displayDeck() {\r\n        for (const auto &card : cards) {\r\n            cout << card.faceValue << \" of \" << card.suit << endl;\r\n        }\r\n    }\r\n\r\n    Card drawCard() {\r\n        if (cards.empty()) {\r\n            string suits[] = {\"Hearts\", \"Diamonds\", \"Clubs\", \"Spades\"};\r\n            string faceValues[] = {\"2\", \"3\", \"4\", \"5\", \"6\", \"7\", \"8\", \"9\", \"10\", \"Jack\", \"Queen\", \"King\", \"Ace\"};\r\n\r\n            for (const auto &suit : suits) {\r\n                for (const auto &faceValue : faceValues) {\r\n                    cards.push_back({suit, faceValue});\r\n                }\r\n            }\r\n            shuffle();\r\n        }\r\n\r\n        Card drawnCard = cards.back();\r\n        cards.pop_back();\r\n        return drawnCard;\r\n    }\r\n};\r\n\r\n//-------------------------------------------------\r\n\r\nint countPoints(const vector<Card>& hand) {\r\n    int points = 0;\r\n    int numAces = 0;\r\n\r\n    for (const auto& card : hand) {\r\n        if (card.faceValue == \"Jack\" || card.faceValue == \"Queen\" || card.faceValue == \"King\") {\r\n            points += 10;\r\n        } else if (card.faceValue == \"Ace\") {\r\n            numAces++;\r\n            points += 11;\r\n        } else {\r\n            points += stoi(card.faceValue);\r\n        }\r\n    }\r\n\r\n    while (points > 21 && numAces > 0) {\r\n        points -= 10;\r\n        numAces--;\r\n    }\r\n    return points;\r\n}\r\n\r\n//-------------------------------------------------\r\n\r\nint dealerTurn(Deck& deck, std::vector<Card>& dealerHand) {\r\n    int sum = 0;\r\n    for (const auto& card : dealerHand) {\r\n        if (card.faceValue == \"Jack\" || card.faceValue == \"Queen\" || card.faceValue == \"King\") {\r\n            sum += 10;\r\n        } else if (card.faceValue == \"Ace\") {\r\n            sum += 11;\r\n        } else {\r\n            sum += std::stoi(card.faceValue);\r\n        }\r\n    }\r\n\r\n    while (sum <= 17) {\r\n        Card newCard = deck.drawCard();\r\n        dealerHand.push_back(newCard);\r\n        sum = countPoints(dealerHand);\r\n    }\r\n    return sum;\r\n}\r\nvoid giveBotInfo(int botId, std::vector<Card>& playerHand, std::vector<Card>& dealerHand){\r\n    \r\n    cout << botId<< endl;\r\n    for (const auto& card : playerHand) {\r\n        cout << card.faceValue.c_str() << \"_\"<<card.suit.c_str() << \" \";\r\n    }\r\n    cout << \"d: \";\r\n    for (const auto& card : dealerHand) {\r\n        cout << card.faceValue.c_str() << \"_\"<<card.suit.c_str()<<\" \";\r\n    }\r\n    cout << endl;\r\n}\r\n\r\n//-------------------------------------------------\r\nDeck deck;\r\ndouble countpoints(std::vector<int>& bot, std::vector<Card>& dealerHand){ //{0 handValue, 1 splitValue, 2 doubledBet, 3 surrendered, 4 insurance};\r\n    int dealer = countPoints(dealerHand);\r\n    \r\n    if (bot[3]) // jeśli się podda\r\n        return -0.5;\r\n        \r\n    if (bot[4])\r\n        if(dealerHand.size() == 2 && dealer == 21) // ubezpieczenie\r\n            return 0;\r\n        else\r\n            return -1.5;\r\n            \r\n    if (bot[1] != 0){ // jeśli jest 2 ręka\r\n        double sum=0;\r\n        if (bot[0] < 22)\r\n            if (bot[0] > dealer || dealer > 21)\r\n                sum += 1;\r\n            else sum -=1;\r\n        else sum -=1;\r\n            \r\n\r\n        if (bot[1] < 22)\r\n            if (bot[1] > dealer || dealer > 21)\r\n                sum += 1;\r\n            else sum -=1;\r\n        else sum -=1;\r\n        return sum;\r\n    }\r\n\r\n    if ((bot[0] > dealer && bot[0] < 22) || (bot[0] < 22 && dealer > 21))\r\n        if (bot[2])\r\n            return 2;\r\n        else return 1;\r\n    else\r\n        if (bot[2])\r\n            return -2;\r\n        else return -1;\r\n}\r\n\r\nvector<int> servebot(int botId, std::vector<Card>& playerHand, std::vector<Card>& dealerHand){\r\n    if(playerHand.size()==0){\r\n        playerHand.push_back(deck.drawCard());\r\n        playerHand.push_back(deck.drawCard());\r\n    }\r\n\r\n    int handValue = countPoints(playerHand);\r\n    int splitValue = 0;\r\n    int doubledBet = 0;\r\n    int surrendered = 0;\r\n    int insurance = 0;\r\n\r\n    if(countPoints(playerHand) == 21)\r\n        return {handValue, splitValue, doubledBet, surrendered, insurance};\r\n\r\n    giveBotInfo(botId, playerHand, dealerHand);\r\n    string response;\r\n    cin >> response;\r\n\r\n    if (response == \"hit\") {\r\n        while(response == \"hit\" && countPoints(playerHand) < 21){\r\n            playerHand.push_back(deck.drawCard());\r\n            giveBotInfo(botId, playerHand, dealerHand);\r\n            cin >> response;\r\n        }\r\n        handValue = countPoints(playerHand);\r\n    } else if (response == \"double\") {\r\n        if(countPoints(playerHand) == 9 ||countPoints(playerHand) == 10 || countPoints(playerHand) == 11){\r\n            playerHand.push_back(deck.drawCard());\r\n            doubledBet = 1;\r\n        }\r\n    } else if (response == \"split\") {\r\n        if (playerHand.size() == 2 && playerHand[0].faceValue == playerHand[1].faceValue) {\r\n            std::vector<Card> splitHand1;\r\n            std::vector<Card> splitHand2;\r\n            splitHand1.push_back(playerHand[0]);\r\n            splitHand2.push_back(playerHand[1]);\r\n            splitHand1.push_back(deck.drawCard());\r\n            splitHand2.push_back(deck.drawCard());\r\n\r\n            giveBotInfo(botId, splitHand1, dealerHand);\r\n            cin >> response;\r\n            while(response == \"hit\"){\r\n                splitHand1.push_back(deck.drawCard());\r\n                giveBotInfo(botId, splitHand1, dealerHand);\r\n                string response;\r\n                cin >> response;\r\n            }\r\n\r\n            giveBotInfo(botId, splitHand2, dealerHand);\r\n            cin >> response;\r\n            while(response == \"hit\"){\r\n                splitHand2.push_back(deck.drawCard());\r\n                giveBotInfo(botId, splitHand2, dealerHand);\r\n                string response;\r\n                cin >> response;\r\n            }\r\n\r\n            handValue = countPoints(splitHand1);\r\n            splitValue = countPoints(splitHand2);\r\n        }\r\n    } else if (response == \"surrender\") {\r\n        surrendered = 1;\r\n    }\r\n      else if (response == \"insurance\") {\r\n        if(countPoints(dealerHand) == 10 || countPoints(dealerHand) == 11)\r\n            insurance = 1;\r\n    }\r\n    return {handValue, splitValue, doubledBet, surrendered, insurance};\r\n}\r\n\r\nint main() {\r\n\r\n    deck.shuffle();\r\n    double bot0money=10;\r\n    double bot1money=10;\r\n    std::vector<Card> dealerHand;\r\n    int turn = 0;\r\n\r\n    vector<int> bot0;\r\n    vector<int> bot1;\r\n    while((turn < 100 || bot0money != bot1money) && turn < 1000){\r\n        turn++;\r\n        dealerHand.push_back(deck.drawCard());\r\n\r\n        vector<Card> playerHand1;\r\n        vector<Card> playerHand2;\r\n\r\n        bot0 = servebot(0, playerHand1, dealerHand);\r\n        bot1 = servebot(1, playerHand2, dealerHand);\r\n\r\n        dealerTurn(deck, dealerHand);\r\n\r\n        bot0money += countpoints(bot0, dealerHand);\r\n        bot1money += countpoints(bot1, dealerHand);\r\n\r\n        if(bot0money < 1 || bot1money < 1)\r\n            break;\r\n        dealerHand = vector<Card>();\r\n    }\r\n\r\n    \r\n    cout << -1 << endl;\r\n    if(bot0money > bot1money)\r\n        cout << 0 << endl;\r\n    else\r\n        cout << 1 << endl;\r\n\r\n    return 0;\r\n}\r\n";
        private string placeholder1 = "placeholder 1 xd";
        private string placeholder2 = "placeholder 2 xdd";
        private string placeholder3 = "placeholder 3 xdd";
        private string bot_zero_py = "import copy\r\n\r\nclass Card:\r\n    def __init__(self, suit, value):\r\n        self.suit = suit\r\n        self.value = value\r\n\r\n    def __repr__(self):\r\n        return f\"{self.value} of {self.suit}\"\r\n    \r\n    def __str__(self):\r\n        return f\"{self.value} of {self.suit}\"\r\n\r\n\r\ndef parse_cards(data):\r\n    hand_cards = []\r\n    cards = data.split('; Table: ')\r\n    #print(cards)\r\n    hand = cards[0].split(';')\r\n    #print(hand)\r\n    for card in hand:\r\n        val = int(card.split(' ')[0])\r\n        suit = card.split(' ')[2]\r\n        hand_cards.append(Card(suit, val))\r\n        #print(f'{val} of {suit}')\r\n    table = [card.strip() for card in cards[1].strip().split(';') if card.strip()]\r\n    table_cards = []\r\n    #print(table)\r\n    for card in table:\r\n        val = int(card.split(' ')[0])\r\n        suit = card.split(' ')[2]\r\n        table_cards.append(Card(suit, val))\r\n    return hand_cards, table_cards\r\n\r\ndef count_points(hand):\r\n    if (len(hand) != 9):\r\n        raise ValueError(\"Wrong number of cards in hand!\")\r\n    points = 0\r\n    distinct_values = set()\r\n    distinct_colors = set()\r\n    color_counts = {}\r\n    value_counts = {}\r\n\r\n    for card in hand:\r\n        distinct_values.add(card.value)\r\n        distinct_colors.add(card.suit)\r\n        color_counts[card.suit] = color_counts.get(card.suit, 0) + 1\r\n        value_counts[card.value] = value_counts.get(card.value, 0) + 1\r\n        \r\n    for card in hand:\r\n        if (color_counts[card.suit] == 5 and value_counts[card.value] == 5):\r\n            return 0\r\n\r\n    for color in distinct_colors:\r\n        if color_counts[color] > 4:\r\n            distinct_values = set()\r\n            value_counts = {}\r\n            for card in hand:\r\n                if (card.suit != color):\r\n                    distinct_values.add(card.value)\r\n                    value_counts[card.value] = value_counts.get(card.value, 0) + 1\r\n            \r\n    for value in distinct_values:\r\n        if value_counts[value] < 5:\r\n            points += int(value)\r\n\r\n    return points\r\n\r\ndef exchange_card(player_hand, table_cards, hand_index, table_index):\r\n    if hand_index < 0 or hand_index >= len(player_hand):\r\n        print(\"Wrong card index.\")\r\n        return\r\n    if table_index < 0 or table_index >= len(table_cards):\r\n        print(\"Wrong table index.\")\r\n        return\r\n    hand = player_hand.copy()\r\n    hand[hand_index] = table_cards[table_index]\r\n    return hand\r\n\r\ndef play():\r\n    while(True):\r\n        #print(\"Wczytuje\")\r\n        status = input()\r\n        hand, table = parse_cards(status)\r\n        #print(\"Wczytane\")\r\n        points = count_points(hand)\r\n        response = \"fold\"\r\n        #print(\"Rozpoczynam liczenie\")\r\n        for i in range (9):\r\n            for j in range (5):\r\n                fake_hand = exchange_card(hand, table, i, j)\r\n                fake_points = count_points(fake_hand)\r\n                if (fake_points <= points):\r\n                    response = str(i) + \" \" + str(j)\r\n                    points = fake_points\r\n        \r\n        #print(\"Koncze liczenie\")\r\n        print(response)\r\nplay()";
        private string bot_zero_2_py = "import copy\r\n\r\nclass Card:\r\n    def __init__(self, suit, value):\r\n        self.suit = suit\r\n        self.value = value\r\n\r\n    def __repr__(self):\r\n        return f\"{self.value} of {self.suit}\"\r\n    \r\n    def __str__(self):\r\n        return f\"{self.value} of {self.suit}\"\r\n\r\n\r\ndef parse_cards(data):\r\n    hand_cards = []\r\n    cards = data.split('; Table: ')\r\n    #print(cards)\r\n    hand = cards[0].split(';')\r\n    #print(hand)\r\n    for card in hand:\r\n        val = int(card.split(' ')[0])\r\n        suit = card.split(' ')[2]\r\n        hand_cards.append(Card(suit, val))\r\n        #print(f'{val} of {suit}')\r\n    table = [card.strip() for card in cards[1].strip().split(';') if card.strip()]\r\n    table_cards = []\r\n    #print(table)\r\n    for card in table:\r\n        val = int(card.split(' ')[0])\r\n        suit = card.split(' ')[2]\r\n        table_cards.append(Card(suit, val))\r\n    return hand_cards, table_cards\r\n\r\ndef count_points(hand):\r\n    if (len(hand) != 9):\r\n        raise ValueError(\"Wrong number of cards in hand!\")\r\n    points = 0\r\n    distinct_values = set()\r\n    distinct_colors = set()\r\n    color_counts = {}\r\n    value_counts = {}\r\n\r\n    for card in hand:\r\n        distinct_values.add(card.value)\r\n        distinct_colors.add(card.suit)\r\n        color_counts[card.suit] = color_counts.get(card.suit, 0) + 1\r\n        value_counts[card.value] = value_counts.get(card.value, 0) + 1\r\n        \r\n    for card in hand:\r\n        if (color_counts[card.suit] == 5 and value_counts[card.value] == 5):\r\n            return 0\r\n\r\n    for color in distinct_colors:\r\n        if color_counts[color] > 4:\r\n            distinct_values = set()\r\n            value_counts = {}\r\n            for card in hand:\r\n                if (card.suit != color):\r\n                    distinct_values.add(card.value)\r\n                    value_counts[card.value] = value_counts.get(card.value, 0) + 1\r\n            \r\n    for value in distinct_values:\r\n        if value_counts[value] < 5:\r\n            points += int(value)\r\n\r\n    return points\r\n\r\ndef exchange_card(player_hand, table_cards, hand_index, table_index):\r\n    if hand_index < 0 or hand_index >= len(player_hand):\r\n        print(\"Wrong card index.\")\r\n        return\r\n    if table_index < 0 or table_index >= len(table_cards):\r\n        print(\"Wrong table index.\")\r\n        return\r\n    hand = player_hand.copy()\r\n    hand[hand_index] = table_cards[table_index]\r\n    return hand\r\n\r\ndef play():\r\n    while(True):\r\n        #print(\"Wczytuje\")\r\n        status = input()\r\n        hand, table = parse_cards(status)\r\n        #print(\"Wczytane\")\r\n        points = count_points(hand)\r\n        response = \"fold\"\r\n        #print(\"Rozpoczynam liczenie\")\r\n        for i in range (9):\r\n            for j in range (5):\r\n                fake_hand = exchange_card(hand, table, i, j)\r\n                fake_points = count_points(fake_hand)\r\n                if (fake_points >= points):\r\n                    response = str(i) + \" \" + str(j)\r\n                    points = fake_points\r\n        \r\n        #print(\"Koncze liczenie\")\r\n        print(response)\r\nplay()";
        private string bot_zero_3_py = "#include <iostream>\r\n#include <cstdlib>\r\n#include <ctime>\r\n#include <string>\r\n\r\nint main() {\r\n    std::string tekst;\r\n\r\n    while(true){\r\n        std::getline(std::cin, tekst);\r\n\r\n        std::srand(std::time(nullptr));\r\n\r\n        int losowa1 = std::rand() % 9;\r\n        int losowa2 = std::rand() % 5;\r\n\r\n        std::cout << losowa1 << \" \" << losowa2 << std::endl;\r\n    }\r\n    return 0;\r\n}\r\n";
        private string zero_for_2_players_py = "import random\r\n\r\nclass Card:\r\n    def __init__(self, suit, value):\r\n        self.suit = suit\r\n        self.value = value\r\n\r\n    def __repr__(self):\r\n        return f\"{self.value} of {self.suit}\"\r\n    \r\n    def __str__(self):\r\n        return f\"{self.value} of {self.suit}\"\r\n\r\nclass Deck:\r\n    def __init__(self):\r\n        self.cards = []\r\n        self.build()\r\n\r\n    def build(self):\r\n        suits = ['Yellow', 'Green', 'Blue', 'Red', 'Pink', 'White', 'Brown']\r\n        values = ['1', '2', '3', '4', '5', '6', '7', '8']\r\n        for suit in suits:\r\n            for value in values:\r\n                self.cards.append(Card(suit, value))\r\n\r\n    def shuffle(self):\r\n        random.shuffle(self.cards)\r\n\r\n    def draw(self):\r\n        if len(self.cards) > 0:\r\n            return self.cards.pop()\r\n        else:\r\n            return None\r\n\r\n    def __len__(self):\r\n        return len(self.cards)\r\n\r\n    def __repr__(self):\r\n        return f\"Deck with {len(self)} cards\"\r\n\r\ndef count_points(hand):\r\n    if (len(hand) != 9):\r\n        raise ValueError(\"Wrong number of cards in hand!\")\r\n    points = 0\r\n    distinct_values = set()\r\n    distinct_colors = set()\r\n    color_counts = {}\r\n    value_counts = {}\r\n\r\n    for card in hand:\r\n        distinct_values.add(card.value)\r\n        distinct_colors.add(card.suit)\r\n        color_counts[card.suit] = color_counts.get(card.suit, 0) + 1\r\n        value_counts[card.value] = value_counts.get(card.value, 0) + 1\r\n        \r\n    for card in hand:\r\n        if (color_counts[card.suit] == 5 and value_counts[card.value] == 5):\r\n            return 0\r\n\r\n    for color in distinct_colors:\r\n        if color_counts[color] > 4:\r\n            distinct_values = set()\r\n            value_counts = {}\r\n            for card in hand:\r\n                if (card.suit != color):\r\n                    distinct_values.add(card.value)\r\n                    value_counts[card.value] = value_counts.get(card.value, 0) + 1\r\n            \r\n    for value in distinct_values:\r\n        if value_counts[value] < 5:\r\n            points += int(value)\r\n\r\n    return points\r\n\r\ndef deal_cards(deck, num_players, num_cards):\r\n    players_hands = [[] for _ in range(num_players)]\r\n    for _ in range(num_cards):\r\n        for i in range(num_players):\r\n            card = deck.draw()\r\n            if card:\r\n                players_hands[i].append(card)\r\n    return players_hands\r\n\r\ndef exchange_card(player_hand, table_cards, hand_index, table_index):\r\n    if hand_index < 0 or hand_index >= len(player_hand):\r\n        print(\"Wrong card index.\")\r\n        return\r\n    if table_index < 0 or table_index >= len(table_cards):\r\n        print(\"Wrong table index.\")\r\n        return\r\n    player_hand[hand_index], table_cards[table_index] = table_cards[table_index], player_hand[hand_index]\r\n    \r\ndef give_bot_info(player, last):\r\n    global hands\r\n    global table_cards\r\n    response = \"\"\r\n    hand = hands[player]\r\n    for card in hand:\r\n        response += str(card)\r\n        response += \";\"\r\n    response += \" Table: \"\r\n    for card in table_cards:\r\n        response += str(card)\r\n        response += \";\"\r\n    if(last):\r\n        response += \"last\"\r\n    print(response)\r\n    \r\ndef select_winner(hands):\r\n    index = 0\r\n    mini = 50\r\n    for i in range(len(hands)):\r\n        if(count_points(hands[i]) < mini):\r\n            index = i\r\n            mini = count_points(hands[i])\r\n    print(index)\r\n    \r\n\r\ndef set_game(players = 4, cards = 9):\r\n    global deck\r\n    deck = Deck()\r\n    deck.shuffle()\r\n    \r\n    global num_players\r\n    global num_cards_per_player\r\n    num_players = players          # valid 2-5 (for currend set of cards)\r\n    num_cards_per_player = cards   # shouldnt be changed, unless with other mechanics\r\n    \r\n    global hands\r\n    global table_cards\r\n    hands = deal_cards(deck, num_players, num_cards_per_player)\r\n    table_cards = [deck.draw() for _ in range(5)]\r\n\r\ndef game(players = 2, cards = 9):\r\n    set_game(players, cards)\r\n    folds = 0\r\n    zero = False\r\n    last = False\r\n    iteration = 0\r\n    while(folds < 2 and iteration < 10000):\r\n        iteration += 1\r\n        for player in range(players):\r\n            print(player)\r\n            if (folds > 1):\r\n                last = True\r\n            give_bot_info(player, last)\r\n            response = input()\r\n            if(response.lower() == 'fold'):\r\n                folds += 1\r\n            else:\r\n                exchange_card(hands[player], table_cards, int(response[0]), int(response[2]))\r\n                if(count_points(hands[player]) == 0):\r\n                    zero = True\r\n                    break\r\n        if(zero):\r\n            break\r\n    print(-1)\r\n    select_winner(hands)\r\n    \r\ndef check_winner():\r\n    global hands\r\n    global num_players\r\n    \r\n    for hand in hands:\r\n        print(count_points(hand))\r\n        \r\ngame()\r\n\r\n# check_winner()  # debug to print players points";
        public Seeder(GathererService gathererService)
        {
            _gatherer = gathererService;
        }

        private async Task AddFile(string file, string name, long id)
        {
            var uploaded = await _gatherer.GetFile(id);
            if (uploaded.Success) { return; }
            byte[] byteArray = Encoding.UTF8.GetBytes(file);
            using (MemoryStream stream = new MemoryStream(byteArray))
            {
                IFormFile formFile = new FormFile(stream, 0, byteArray.Length, "name", "filename.txt");
                var res = await _gatherer.SaveFile(formFile);
                if (res.Success)
                {
                    string cont = res.Data.ToString();
                    long botFileId = Convert.ToInt32(cont);
                    Console.WriteLine($"File {name} added with id {botFileId}");
                }
                else
                {
                    Console.WriteLine($"Uploading to FileGatherer failed {res.Message}");
                }
            }
        }

        public async Task Seed()
        {
            await AddFile(placeholder0, "placeholder0.xd", 1);
            await AddFile(bot1Bj_cpp, "bot1BJ.cpp", 2);
            await AddFile(bot2Bj_cpp, "bot2BJ.cpp", 3);
            await AddFile(bot3Bj_cpp, "bot3BJ.cpp", 4);
            await AddFile(black_jack_cpp, "BlackJack.cpp", 5);
            await AddFile(placeholder1, "placeholder0.xd", 6);
            await AddFile(placeholder2, "placeholder0.xd", 7);
            await AddFile(placeholder3, "placeholder0.xd", 8);
            await AddFile(bot_zero_py, "bot_zero.py", 9);
            await AddFile(bot_zero_2_py, "bot_zero_2.py", 10);
            await AddFile(bot_zero_3_py, "bot_zero_3.py", 11);
            await AddFile(zero_for_2_players_py, "zero_for_2_players.py", 12);
        }
    }
}
