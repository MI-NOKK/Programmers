using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programmers.level_2
{
     class pick__tangerines
    {
        public int solution(int k, int[] tangerine)
        {
            int answer = 0;
            Dictionary<int, int> checkCount = new Dictionary<int, int>();

            foreach (int size in tangerine)
            {
                if (!checkCount.ContainsKey(size))
                {
                    checkCount[size] = 0;
                }
                checkCount[size]++;
            }

            var sortDict = checkCount.OrderByDescending(x => x.Value);
            int loop = 0;
            foreach (var size in sortDict)
            {
                loop++;
                k -= size.Value;
                if (k < 0)
                {
                    answer = loop;
                    break;
                }
              
            }

            return answer;
        }
    }
}
