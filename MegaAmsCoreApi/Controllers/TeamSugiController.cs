using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MegaAmsCoreApi.Services;
using System.Data;
using System.Data.SqlClient;
using MegaAmsCoreApi.Models;
using MegaAmsCoreApi.Attributes;
using Microsoft.Extensions.Logging;

namespace MegaAmsCoreApi.Controllers
{
 
    [Route("api/[controller]")]
    [ApiController]
    public class TeamSugiController : ControllerBase
    {
        private readonly IDapper _dapper;
        private readonly ILogger<TeamSugiController> _logger;
        public string logStr;

        public TeamSugiController(IDapper dapper, ILogger<TeamSugiController> logger)
        {
            _dapper = dapper;
            _logger = logger;
        }
       
        
        [HttpGet(nameof(GetByYear))]
        public async Task<IEnumerable<TeamSugi>> GetByYear(int vYear)
        {
            IEnumerable<TeamSugi> sugis;
            var dbparams = new DynamicParameters();
            dbparams.Add("YEAR", vYear, DbType.Int32);
            dbparams.Add("MAIN_YN", "N", DbType.String);
            dbparams.Add("TOP_YN", "", DbType.String);

            sugis = await Task.FromResult(_dapper.GetAll<TeamSugi>("[dbo].[MSP_MG_BOARD_SUGI]",
                                                dbparams,
                                                commandType: CommandType.StoredProcedure));
            if(sugis == null)
            {
                logStr = string.Format("GetByYear log {0} {1}", vYear, "데이터가 존재하지 않습니다.");
                _logger.LogWarning(logStr);
                
            }
            _logger.LogInformation(sugis.ToString());
            logStr = string.Format("GetByYear log {0} {1}", vYear, "test");

            return sugis;
        }

        [HttpGet("{id:int}")]
        public async Task<TeamSugiDTO> GetById(int Id)
        {
            TeamSugiDTO dto = new TeamSugiDTO();
            var result = await Task.FromResult(_dapper.Get<TeamSugi>($"Select * from [MG_BOARD_SUGI] where Seq = {Id}", null, commandType: CommandType.Text));

            if (result == null)
            {
                dto.errCode = -300;
                dto.errMsg = "데이터가 존재하지 않습니다.";

                logStr = string.Format("{0} {1} GetById {2} ", dto.errCode, dto.errMsg, Id);
                _logger.LogWarning(logStr);

                return dto;

            }

            dto.errCode = 100;
            dto.errMsg = "success";
            dto.teamsugi = result;

            logStr = string.Format("{0} {1} GetById", dto.errCode, dto.errMsg);
            _logger.LogInformation(logStr);

            return dto;
        }

        [HttpGet]
        public async Task<TeamSugiListDTO> GetAll()
        {
            IEnumerable<TeamSugi> sugis;
            var dbparams = new DynamicParameters();
            dbparams.Add("YEAR", "2020", DbType.Int32);
            dbparams.Add("MAIN_YN", "Y", DbType.String);
            dbparams.Add("TOP_YN", "", DbType.String);
            

            sugis = await Task.FromResult(_dapper.GetAll<TeamSugi>("[dbo].[MSP_MG_BOARD_SUGI]",
                                                dbparams,
                                                commandType: CommandType.StoredProcedure));
            TeamSugiListDTO dto = new TeamSugiListDTO();
            dto.errCode = 100;
            dto.errMsg = "success";
            dto.sugiList = sugis;

            logStr = string.Format("{0} {1} ReadAllddd", dto.errCode, dto.errMsg);
            _logger.LogInformation(logStr);
            return dto;
        }

        [HttpDelete("{id:int}")]
        public async Task<ResultDTO> Delete(int Id)
        {
            var result = await Task.FromResult(_dapper.Delete<int>($"DELETE FROM [dbo].[MG_BOARD_SUGI] WHERE Seq = {Id}", null, commandType: CommandType.Text));

            ResultDTO dto = new ResultDTO();
            if (result != 0)
            {
                dto.errCode = -300;
                dto.errMsg = "데이터 삭제에 문제가 발생되었습니다.";

                logStr = string.Format("{0} {1} Delete {2} {3} ", dto.errCode, dto.errMsg, Id, result);
                _logger.LogWarning(logStr);

                return dto;

            }

            dto.errCode = 100;
            dto.errMsg = "success";
            return dto;
        }

        [HttpPost(nameof(Create))]
        public async Task<ResultDTO> Create(TeamSugiCreate data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("S_CODENAME", data.S_CODENAME, DbType.String);
            dbparams.Add("S_NAME", data.S_NAME, DbType.String);
            dbparams.Add("S_UNIV", data.S_UNIV, DbType.String);
            dbparams.Add("S_MAJOR", data.S_MAJOR, DbType.String);
            dbparams.Add("S_DEPT_TYPE", data.S_DEPT_TYPE, DbType.String);
            dbparams.Add("S_TYPE", data.S_TYPE, DbType.String);
            dbparams.Add("S_SUBJECT", data.S_SUBJECT, DbType.String);
            dbparams.Add("S_CONTENT", data.S_CONTENT, DbType.String);
            dbparams.Add("S_YEAR", data.S_YEAR, DbType.String);
            dbparams.Add("S_MAINYN", data.S_MAINYN, DbType.String);

            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[MSP_MG_BOARD_SUGI_INSERT]",
                                                dbparams,
                                                commandType: CommandType.StoredProcedure));

            ResultDTO dto = new ResultDTO();
            if (result != 0)
            {
                dto.errCode = -300;
                dto.errMsg = "데이터 입력에 문제가 발생되었습니다.";

                logStr = string.Format("{0} {1} Create {2}", dto.errCode, dto.errMsg, result);
                _logger.LogWarning(logStr);

                return dto;

            }

            dto.errCode = 100;
            dto.errMsg = "success";
            return dto;
        }

        [HttpPut(nameof(Update))]
        public async Task<ResultDTO> Update(TeamSugi data)
        {
            var dbPara = new DynamicParameters();
            //dbPara.Add("Seq", data.Seq, DbType.Int32);  
            //dbPara.Add("S_CONTENT", data.S_CONTENT, DbType.String);  

            var result = await Task.FromResult(_dapper.Update<int>($"UPDATE MG_BOARD_SUGI SET S_CONTENT = '{data.S_CONTENT}' WHERE SEQ = {data.Seq}", null, CommandType.Text));

            ResultDTO dto = new ResultDTO();
            if (result != 0)
            {
                dto.errCode = -300;
                dto.errMsg = "데이터 수정에 문제가 발생되었습니다.";

                logStr = string.Format("{0} {1} Delete {2} ", dto.errCode, dto.errMsg,  result);
                _logger.LogWarning(logStr);

                return dto;

            }

            dto.errCode = 100;
            dto.errMsg = "success";
            return dto;
        }
    }
}
