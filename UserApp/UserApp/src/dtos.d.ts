/* Options:
Date: 2018-07-22 19:57:54
Version: 5.10
Tip: To override a DTO option, remove "//" prefix before updating
BaseUrl: http://localhost:39324

//GlobalNamespace: 
//MakePropertiesOptional: True
//AddServiceStackTypes: True
//AddResponseStatus: False
//AddImplicitVersion: 
//AddDescriptionAsComments: True
//IncludeTypes: 
//ExcludeTypes: 
//DefaultImports: 
*/


interface IReturn<T>
{
}

interface IReturnVoid
{
}

interface IPost
{
}

// @DataContract
interface ResponseError
{
    // @DataMember(Order=1, EmitDefaultValue=false)
    errorCode?: string;

    // @DataMember(Order=2, EmitDefaultValue=false)
    fieldName?: string;

    // @DataMember(Order=3, EmitDefaultValue=false)
    message?: string;

    // @DataMember(Order=4, EmitDefaultValue=false)
    meta?: { [index:string]: string; };
}

// @DataContract
interface ResponseStatus
{
    // @DataMember(Order=1)
    errorCode?: string;

    // @DataMember(Order=2)
    message?: string;

    // @DataMember(Order=3)
    stackTrace?: string;

    // @DataMember(Order=4)
    errors?: ResponseError[];

    // @DataMember(Order=5)
    meta?: { [index:string]: string; };
}

interface UserResponse
{
    result?: string;
}

// @DataContract
interface AuthenticateResponse
{
    // @DataMember(Order=1)
    userId?: string;

    // @DataMember(Order=2)
    sessionId?: string;

    // @DataMember(Order=3)
    userName?: string;

    // @DataMember(Order=4)
    displayName?: string;

    // @DataMember(Order=5)
    referrerUrl?: string;

    // @DataMember(Order=6)
    bearerToken?: string;

    // @DataMember(Order=7)
    refreshToken?: string;

    // @DataMember(Order=8)
    responseStatus?: ResponseStatus;

    // @DataMember(Order=9)
    meta?: { [index:string]: string; };
}

// @DataContract
interface AssignRolesResponse
{
    // @DataMember(Order=1)
    allRoles?: string[];

    // @DataMember(Order=2)
    allPermissions?: string[];

    // @DataMember(Order=3)
    responseStatus?: ResponseStatus;
}

// @DataContract
interface UnAssignRolesResponse
{
    // @DataMember(Order=1)
    allRoles?: string[];

    // @DataMember(Order=2)
    allPermissions?: string[];

    // @DataMember(Order=3)
    responseStatus?: ResponseStatus;
}

// @DataContract
interface RegisterResponse
{
    // @DataMember(Order=1)
    userId?: string;

    // @DataMember(Order=2)
    sessionId?: string;

    // @DataMember(Order=3)
    userName?: string;

    // @DataMember(Order=4)
    referrerUrl?: string;

    // @DataMember(Order=5)
    bearerToken?: string;

    // @DataMember(Order=6)
    refreshToken?: string;

    // @DataMember(Order=7)
    responseStatus?: ResponseStatus;

    // @DataMember(Order=8)
    meta?: { [index:string]: string; };
}

// @Route("/user")
interface User extends IReturn<UserResponse>
{
    name?: string;
    id?: string;
    userIds?: string[];
}

// @Route("/auth")
// @Route("/auth/{provider}")
// @Route("/authenticate")
// @Route("/authenticate/{provider}")
// @DataContract
interface Authenticate extends IReturn<AuthenticateResponse>, IPost
{
    // @DataMember(Order=1)
    provider?: string;

    // @DataMember(Order=2)
    state?: string;

    // @DataMember(Order=3)
    oauth_token?: string;

    // @DataMember(Order=4)
    oauth_verifier?: string;

    // @DataMember(Order=5)
    userName?: string;

    // @DataMember(Order=6)
    password?: string;

    // @DataMember(Order=7)
    rememberMe?: boolean;

    // @DataMember(Order=8)
    continue?: string;

    // @DataMember(Order=9)
    nonce?: string;

    // @DataMember(Order=10)
    uri?: string;

    // @DataMember(Order=11)
    response?: string;

    // @DataMember(Order=12)
    qop?: string;

    // @DataMember(Order=13)
    nc?: string;

    // @DataMember(Order=14)
    cnonce?: string;

    // @DataMember(Order=15)
    useTokenCookie?: boolean;

    // @DataMember(Order=16)
    accessToken?: string;

    // @DataMember(Order=17)
    accessTokenSecret?: string;

    // @DataMember(Order=18)
    meta?: { [index:string]: string; };
}

// @Route("/assignroles")
// @DataContract
interface AssignRoles extends IReturn<AssignRolesResponse>, IPost
{
    // @DataMember(Order=1)
    userName?: string;

    // @DataMember(Order=2)
    permissions?: string[];

    // @DataMember(Order=3)
    roles?: string[];
}

// @Route("/unassignroles")
// @DataContract
interface UnAssignRoles extends IReturn<UnAssignRolesResponse>, IPost
{
    // @DataMember(Order=1)
    userName?: string;

    // @DataMember(Order=2)
    permissions?: string[];

    // @DataMember(Order=3)
    roles?: string[];
}

// @Route("/register")
// @DataContract
interface Register extends IReturn<RegisterResponse>, IPost
{
    // @DataMember(Order=1)
    userName?: string;

    // @DataMember(Order=2)
    firstName?: string;

    // @DataMember(Order=3)
    lastName?: string;

    // @DataMember(Order=4)
    displayName?: string;

    // @DataMember(Order=5)
    email?: string;

    // @DataMember(Order=6)
    password?: string;

    // @DataMember(Order=7)
    autoLogin?: boolean;

    // @DataMember(Order=8)
    continue?: string;
}
