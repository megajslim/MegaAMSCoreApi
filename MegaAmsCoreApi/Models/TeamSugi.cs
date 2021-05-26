using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MegaAmsCoreApi.Models
{
    public class TeamSugiListDTO
    {
        public int errCode { get; set; }
        public string errMsg { get; set; }
        public IEnumerable<TeamSugi> sugiList { get; set; }
    }

    public class TeamSugiDTO
    {
        public int errCode { get; set; }
        public string errMsg { get; set; }
        public TeamSugi teamsugi { get; set; }

    }

    public class TeamSugi
    {
        public int Seq { get; set; }
        public string S_CODE { get; set; } //학원
        public string S_CODENAME { get; set; } //학원
        public string S_NAME { get; set; } //등록자
        public string S_UNIV { get; set; } //대학명
        public string S_MAJOR { get; set; } //학과
        public string S_SUBJECT { get; set; } //한줄멘트
        public string S_CONTENT { get; set; } //덕분에내용
        public string S_TYPE { get; set; }
        public string S_DEPT_TYPE { get; set; } //수시/정시
        public string S_TOPYN { get; set; }

    }

    public class TeamSugiCreate
    {
        public string S_CODENAME { get; set; } //학원
        public string S_NAME { get; set; } //등록자
        public string S_UNIV { get; set; } //대학명
        public string S_MAJOR { get; set; } //학과
        public string S_DEPT_TYPE { get; set; } //수시/정시
        public string S_TYPE { get; set; }
        public string S_YEAR { get; set; }
        public string S_SUBJECT { get; set; } //한줄멘트
        public string S_CONTENT { get; set; } //덕분에내용
        public string S_MAINYN { get; set; }

    }

    public class ResultDTO
    {
        public int errCode { get; set; }
        public string errMsg { get; set; }
    }

}
