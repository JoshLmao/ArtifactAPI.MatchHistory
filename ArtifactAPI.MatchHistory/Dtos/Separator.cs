﻿namespace ArtifactAPI.MatchHistory.Dtos
{
    class Separator : ListItem
    {
        public string Header { get; set; }
        public string HeaderTwo { get; set; }
        public string HeaderThree { get; set; }

        public Separator(string h, string h2, string h3)
        {
            Header = h;
            HeaderTwo = h2;
            HeaderThree = h3;
        }
    }
}
