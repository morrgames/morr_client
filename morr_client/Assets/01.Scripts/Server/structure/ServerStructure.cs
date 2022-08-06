using System.Collections.Generic;

public class SendBase
{
    public int cmd { get; set; }
    public int aidx { get; set; }
    public string session { get; set; }
}

public class RecvBase
{
    public int cmd { get; set; }
    public int status { get; set; }
}

public class SEND_REGID_DATA : SendBase
{
    public string login_id { get; set; }
    public string uuid { get; set; }
    public int login_type { get; set; }
    public string name { get; set; }
}

public class RECV_REGID_DATA : RecvBase
{
    public Data data { get; set; }

    public class Data
    {
        public string name { get; set; }
    }
}

public class SEND_LOGIN_DATA : SendBase
{
    public int os_type { get; set; }
    public string login_id { get; set; }
    public int login_type { get; set; }
    public string uuid { get; set; }
}

public class RECV_LOGIN_DATA : RecvBase
{
    public Data data { get; set; }

    public class Data
    {
        public UserInfo user_info { get; set; }
    }

    public class UserInfo
    {
        public int aidx { get; set; }
        public string name { get; set; }
        public string login_id { get; set; }
        public int login_type { get; set; }
        public string uuid { get; set; }
        public string session { get; set; }
    }
}

public class SEND_ITEM_LIST_DATA : SendBase
{
    //
}

public class RECV_ITEM_LIST_DATA : RecvBase
{
    public Data data { get; set; }

    public class Data
    {
        public List<Item> item_list { get; set; }
    }

    public class Item
    {
        public int idx { get; set; }
        public int item_code { get; set; }
        public int cnt { get; set; }
    }
}

public class SEND_GET_EXP_DATA : SendBase
{
    //
}

public class RECV_GET_EXP_DATA : RecvBase
{
    public Data data { get; set; }

    public class Data
    {
        public int exp { get; set; }
    }
}

public class SEND_UNIT_LIST_DATA : SendBase
{
    //
}

public class RECV_UNIT_LIST_DATA : RecvBase
{
    public Data data { get; set; }

    public class Data
    {
        public List<Unit> unit_list { get; set; } = new List<Unit>();
    }

    public class Unit
    {
        public int unit_code { get; set; }
    }
}

public class SEND_UNIT_TRANS_DATA : SendBase
{
    public int item_idx { get; set; }
}

public class RECV_UNIT_TRANS_DATA : RecvBase
{
    public Data data { get; set; }

    public class Data
    {
        //
    }
}

public enum RequestType
{
    GET,
    POST,
}

public enum PacketCode
{
    REG_ID = 101,
    LOGIN = 103,
    ITEM_LIST = 201,
    GET_EXP = 203,
    UNIT_LIST = 301,
    UNIT_TRANS = 303,
    SUCCESS = 200,

    //ERROR.
    ERR_critical_data_valid = -100, // 데이타 오류
    ERR_wrong_packet = -101, // 잘못된 패킷오류
    ERR_nohandler = -102, // 없는 프로토콜
    ERR_unknown = -103, // 알수없는 에러 : 예외처리 하지 않은 모든오류 - 서버 담당자에게 문의
    ERR_validator = -104, // request 파라메터가 잘못되었다. : validator 에서 걸러진 경우
    ERR_crypto = -108, // 암호화 복호화 과정 오류
    ERR_nodata = -109, // 빈데이타 보냄
    ERR_session_invalid = -110, // redis 세션 데이타가 존재하지 않는다.
    ERR_session = -111, // client 에서 보내준 세션값이 잘못되었다. : 다른유저가 같은 계정으로 중복로그인일 확률이큼
    ERR_parameter = -112, // validator 를 통과 했지만 이후에서 체크한 파라메터에서 오류가 났을때
    ERR_logical = -113, // 참조 테이블 데이타 등의 로직적인 오류

    ERR_server_login_block = -200, // 서버 로그인이 잠겨있습니다.
    ERR_version = -201, // 로그인 버전이 틀립니다. : 다운로드 페이지로 이동해주세요
    ERR_server_inspect = -202, // 서버 점검중 입니다.
    ERR_server_regid_block = -204, // 서버에 계정생성이 잠겨 있습니다.

    ERR_not_found_id = -1023, // 계정정보를 찾을수 없습니다.
    ERR_regid_overlap_id = -1024, // 계정 생성시 아이디 중복
    ERR_regid_overlap_name = -1025, // 계정 생성시 네임 중복
    ERR_overlap_link_id = -1026, // 이미 연동된 sns id

    ERR_block_user = -1030, // 블럭된 유저입니다.

    ERR_not_found_item_list = -1031, // 아이템이 존재하지 않습니다.
    ERR_not_found_unit_list = -1032, // 유닛이 존재하지 않습니다.
    ERR_not_found_my_exp = -1033, // 경험치가 존재하지 않습니다.
}