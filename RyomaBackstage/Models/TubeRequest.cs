using System.Collections.Generic;

namespace RyomaBackstage.Models
{
    public class TubeRequest
    {
        public int TubeLength { get; set; }
        public List<TubeSetting> TubeSettings { get; set; }

        public TubeRequest()
        {
            TubeLength = 1000;
        }
    }
}