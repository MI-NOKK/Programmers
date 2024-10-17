using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programmers.level_1
{
    class walk_in_the_park
    {
        private int[] findStartPoint(string[] park)
        {
            int[] answer = new int[2];

            for (int i = 0; i < park.Length; i++)
            {
                int loop = 0;
                foreach (char check in park[i])
                {
                    if (check == 'S')
                    {
                        answer[0] = i;
                        answer[1] = loop;

                        break;
                    }

                    loop++;
                }
            }

            return answer;
        }

        private int[] move(string[] park, string[] routes, int[] startPoint)
        {
            int[] answer = startPoint;

            foreach (string route in routes)
            {
                string op = route.Split(' ')[0];
                int n = int.Parse(route.Split(' ')[1]);
                string parkPath = "";
                bool check = false;

                switch (op)
                {
                    case "E":
                        parkPath = park[answer[0]];
                        if (answer[1] + n >= parkPath.Length)
                        {
                            break;
                        }
                        
                        for (int i = 1; i < n + 1; i++)
                        {
                            if (parkPath[answer[1] + i] == 'X')
                            {
                                check = false;
                                break;
                            }
                            else
                            {
                                check = true;
                            }
                        }

                        if(check)
                        {
                            answer[1] += n;
                        }

                        break;
                    case "W":
                         parkPath = park[answer[0]];
                        if (answer[1] - n  < 0)
                        {
                            break;
                        }

                        for (int i = 1; i < n + 1; i++)
                        {
                            if (parkPath[answer[1] - i] == 'X')
                            {
                                check = false;
                                break;
                            }
                            else
                            {
                                check = true;
                            }
                        }

                        if (check)
                        {
                            answer[1] -= n;
                        }
                        break;
                    case "S":
                        if (answer[0] + n >= park.Length)
                        {
                            break;
                        }
                        for (int i = 1; i < n + 1; i++)
                        {
                            
                            if (park[answer[0] + i][answer[1]] == 'X')
                            {
                                check = false;
                                break;
                            }
                            else
                            {
                                check = true;
                            }
                        }
                        if(check)
                        {
                            answer[0] += n;
                        }

                        break;
                    case "N":
                        if (answer[0] - n < 0)
                        {
                            break;
                        }
                        for (int i = 1; i < n + 1; i++)
                        {

                            if (park[answer[0] - i][answer[1]] == 'X')
                            {
                                check = false;
                                break;
                            }
                            else
                            {
                                check = true;
                            }
                        }
                        if (check)
                        {
                            answer[0] -= n;
                        }
                        break;
                }
            }

            return answer;
        }

        public int[] solution(string[] park, string[] routes)
        {
            int[] answer = new int[] { };

            int[] startPoint = findStartPoint(park);

            answer = move(park, routes, startPoint);

            return answer;
        }
    }
}
