using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programmers
{
    /*
     * 문제 설명**
        선물을 직접 전하기 힘들 때 카카오톡 선물하기 기능을 이용해 축하 선물을 보낼 수 있습니다. 
        당신의 친구들이 이번 달까지 선물을 주고받은 기록을 바탕으로 다음 달에 누가 선물을 많이 받을지 예측하려고 합니다.

        두 사람이 선물을 주고받은 기록이 있다면, 이번 달까지 두 사람 사이에 더 많은 선물을 준 사람이 다음 달에 선물을 하나 받습니다.
        예를 들어 A가 B에게 선물을 5번 줬고, B가 A에게 선물을 3번 줬다면 다음 달엔 A가 B에게 선물을 하나 받습니다.
        두 사람이 선물을 주고받은 기록이 하나도 없거나 주고받은 수가 같다면, 선물 지수가 더 큰 사람이 선물 지수가 더 작은 사람에게 선물을 하나 받습니다.
        선물 지수는 이번 달까지 자신이 친구들에게 준 선물의 수에서 받은 선물의 수를 뺀 값입니다.
        예를 들어 A가 친구들에게 준 선물이 3개고 받은 선물이 10개라면 A의 선물 지수는 -7입니다. B가 친구들에게 준 선물이 3개고 받은 선물이 2개라면 B의 선물 지수는 1입니다. 만약 A와 B가 선물을 주고받은 적이 없거나 정확히 같은 수로 선물을 주고받았다면, 다음 달엔 B가 A에게 선물을 하나 받습니다.
        만약 두 사람의 선물 지수도 같다면 다음 달에 선물을 주고받지 않습니다.
        위에서 설명한 규칙대로 다음 달에 선물을 주고받을 때, 당신은 선물을 가장 많이 받을 친구가 받을 선물의 수를 알고 싶습니다.

        친구들의 이름을 담은 1차원 문자열 배열 friends 이번 달까지 친구들이 주고받은 선물 기록을 담은 1차원 문자열 배열 gifts가 매개변수로 주어집니다. 이때, 다음달에 가장 많은 선물을 받는 친구가 받을 선물의 수를 return 하도록 solution 함수를 완성해 주세요.

        **제한사항**
        *2 ≤ friends의 길이 = 친구들의 수 ≤ 50
        friends의 원소는 친구의 이름을 의미하는 알파벳 소문자로 이루어진 길이가 10 이하인 문자열입니다.
        이름이 같은 친구는 없습니다.
        *1 ≤ gifts의 길이 ≤ 10,000
        gifts의 원소는 "A B"형태의 문자열입니다. A는 선물을 준 친구의 이름을 B는 선물을 받은 친구의 이름을 의미하며 공백 하나로 구분됩니다.
        A와 B는 friends의 원소이며 A와 B가 같은 이름인 경우는 존재하지 않습니다.
     */
    class most_received_gift
    {
        /*
         1.선물을 더 많이 준 사람이 다음달에 선물 받음
         2.선물 기록이 없거나 같으면 선물 지수가 높은 사람이 선물 받음
         ㄴ선물 지수: 자신이 준 선물 수 - 받은 선물 수
         3.선물 지수도 같으면 주고받지 않음
 
         result 선물을 가장 많이 받을 친구가 받을 선물의 수
 
         인수
            string[] friends : 친구들 이름 (2<= x <=50, 동일인 없음)
            string[] gifts : 친구들이 주고 받은 선물 기록 (1<= x <=10000, "선물준사람 선물받은사람" 형태 )
        */

        // 선물 주고받은 기록을 분석하여 가장 많이 선물을 받을 친구가 받을 선물 수를 계산
        public int solution(string[] friends, string[] gifts)
        {
            var giftGraph = InitializegiftGraph(friends);
            var receiveCount = InitializeCount(friends);
            var giveCount = InitializeCount(friends);

            // 2. 선물 기록을 처리
            ProcessGifts(gifts, giftGraph, giveCount, receiveCount);

            // 3. 다음 달에 받을 선물 수 계산
            var nextMonthReceive = PredictNextMonthGifts(friends, giftGraph, giveCount, receiveCount);

            // 4. 가장 많이 선물을 받을 친구의 선물 수 찾기
            return GetMaxGifts(nextMonthReceive);
        }

        // 1. 각 친구가 다른 친구에게 준 선물 수를 저장할 딕셔너리를 초기화하는 함수
        private Dictionary<string, Dictionary<string, int>> InitializegiftGraph(string[] friends)
        {
            var giftGraph = new Dictionary<string, Dictionary<string, int>>();
            foreach (var friend in friends)
            {
                giftGraph[friend] = new Dictionary<string, int>();
            }
            return giftGraph;
        }

        // 2. 각 친구의 준 선물 수 또는 받은 선물 수를 기록할 딕셔너리 초기화
        private Dictionary<string, int> InitializeCount(string[] friends)
        {
            var count = new Dictionary<string, int>();
            foreach (var friend in friends)
            {
                count[friend] = 0;
            }
            return count;
        }

        // 3. gifts 배열을 처리하여 각 친구가 주고받은 선물 횟수를 업데이트하는 함수
        private void ProcessGifts(string[] gifts, Dictionary<string, Dictionary<string, int>> giftGraph, Dictionary<string, int> giveCount, Dictionary<string, int> receiveCount)
        {
            foreach (var gift in gifts)
            {
                var parts = gift.Split(' ');
                var giver = parts[0];
                var receiver = parts[1];

                if (!giftGraph[giver].ContainsKey(receiver))
                {
                    giftGraph[giver][receiver] = 0;
                }
                giftGraph[giver][receiver]++;
                giveCount[giver]++;
                receiveCount[receiver]++;
            }
        }

        // 4. 다음 달에 받을 선물 수를 예측하는 함수
        private Dictionary<string, int> PredictNextMonthGifts(string[] friends, Dictionary<string, Dictionary<string, int>> giftGraph, Dictionary<string, int> giveCount, Dictionary<string, int> receiveCount)
        {
            var nextMonthReceive = InitializeCount(friends);

            for (int i = 0; i < friends.Length; i++)
            {
                for (int j = i + 1; j < friends.Length; j++)
                {
                    var friendA = friends[i];
                    var friendB = friends[j];

                    int giftsAB = giftGraph[friendA].ContainsKey(friendB) ? giftGraph[friendA][friendB] : 0;
                    int giftsBA = giftGraph[friendB].ContainsKey(friendA) ? giftGraph[friendB][friendA] : 0;

                    if (giftsAB > giftsBA)
                    {

                        nextMonthReceive[friendA]++;
                    }
                    else if (giftsAB < giftsBA)
                    {
                        nextMonthReceive[friendB]++;
                    }
                    else
                    {
                        int giftIndexA = giveCount[friendA] - receiveCount[friendA];
                        int giftIndexB = giveCount[friendB] - receiveCount[friendB];

                        if (giftIndexA > giftIndexB)
                        {
                            nextMonthReceive[friendA]++;
                        }
                        else if (giftIndexA < giftIndexB)
                        {
                            nextMonthReceive[friendB]++;
                        }
                        // 선물 지수가 같으면 선물 주고받지 않음
                    }
                }
            }

            return nextMonthReceive;
        }

        // 5. 가장 많이 선물을 받을 친구의 선물 수를 반환하는 함수
        private int GetMaxGifts(Dictionary<string, int> nextMonthReceive)
        {
            int maxGifts = 0;
            foreach (var count in nextMonthReceive.Values)
            {
                if (count > maxGifts)
                {
                    maxGifts = count;
                }
            }
            return maxGifts;
        }
    }
}
