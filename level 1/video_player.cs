using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programmers.level_1
{
    internal class video_player
    {
        private int TimeToSeconds(string time)
        {
            var parts = time.Split(':');
            int minutes = int.Parse(parts[0]);
            int seconds = int.Parse(parts[1]);
            return minutes * 60 + seconds;
        }

        // 초 단위를 mm:ss 형식의 문자열로 변환하는 함수
        private string SecondsToTime(int totalSeconds)
        {
            int minutes = totalSeconds / 60;
            int seconds = totalSeconds % 60;
            return $"{minutes:D2}:{seconds:D2}";
        }

        /*
        *   video_len : 비디오 길이
        *   pos       : 직전의 재생위치
        *   op_start  : 오프닝 시작 시각
        *   op_end    : 오프닝이 끝나는 시각
        *   commands  : 사용자의 입력
        */
        public string solution(string video_len, string pos, string op_start, string op_end, string[] commands)
        {
            // 입력된 문자열을 초 단위로 변환
            int videoLength = TimeToSeconds(video_len);
            int position = TimeToSeconds(pos);
            int openingStart = TimeToSeconds(op_start);
            int openingEnd = TimeToSeconds(op_end);

            // 명령어 처리
            foreach (var command in commands)
            {
                if (command == "prev")
                {
                    // prev 명령어 처리: 10초 감소 (최소 0초)
                    position = Math.Max(0, position - 10);
                }
                else if (command == "next")
                {
                    if (position >= openingStart && position <= openingEnd)
                    {
                        position = openingEnd;
                    }
                    // next 명령어 처리: 10초 증가 (최대 비디오 길이까지)
                    position = Math.Min(videoLength, position + 10);
                }

                // 오프닝 구간에 있으면 오프닝 끝 시각으로 이동
                if (position >= openingStart && position <= openingEnd)
                {
                    position = openingEnd;
                }
            }

            // 최종 위치를 mm:ss 형식으로 변환하여 반환
            return SecondsToTime(position);
        }
    }

}
