using System;
public enum RequestType
{
    GET,
    POST,
}
[Serializable]
public class REGID_REQ
{
    public int cmd;
    public string login_id;
    public string uuid;
    public int login_type;
    public string name;
}
[Serializable]
public class REGID_RES
{
    //todo.
}

[Serializable]
public class LOGIN_REQ
{
    public int cmd;
    public string login_id;
    public string uuid;
    public int login_type;
    public int os_type;
}
[Serializable]
public class LOGIN_RES
{
   //todo.
}