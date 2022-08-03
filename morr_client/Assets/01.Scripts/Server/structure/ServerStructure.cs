using System;
public enum RequestType
{
    GET,
    POST,
}

public class ResBase
{
    public int cmd;
    public int status;
}
public class ReqBase
{
    public int cmd;
    public int aidx;
    public string session;
 }
[Serializable]
public class REGID_REQ : ReqBase
{
    public string login_id;
    public string uuid;
    public int login_type;
    public string name;
}
[Serializable]
public class REGID_RES : ResBase
{
    public Data data;

    [Serializable]
    public class Data
    {
        public string name;//흠..
    }
}

[Serializable]
public class LOGIN_REQ :ReqBase
{
    public string login_id;
    public string uuid;
    public int login_type;
}
[Serializable]
public class LOGIN_RES : ResBase
{
    public Data data;

    [Serializable]
    public class Data
    {
        public UserInfo user_info;
    }

    [Serializable]
    public class UserInfo
    {
        public int aidx;
        public string name;
        public string login_id;
        public int login_type;
        public string uuid;
        public string session;
    }
}