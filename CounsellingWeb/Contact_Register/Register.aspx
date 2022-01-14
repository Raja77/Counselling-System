<%@ Page Title="Register on Couselling Portal" Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="CounsellingWeb.Register" %>
<html lang="en">

<head>
    <title>Welcome to Counselling Portal - Register </title>
    <!-- HTML5 Shim and Respond.js IE10 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 10]>
      <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
      <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
      <![endif]-->
      <!-- Meta -->
      <meta charset="utf-8">
      <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimal-ui">
      <meta http-equiv="X-UA-Compatible" content="IE=edge" />
      <meta name="description" content="Mega Able Bootstrap admin template made using Bootstrap 4 and it has huge amount of ready made feature, UI components, pages which completely fulfills any dashboard needs." />
      <meta name="keywords" content="bootstrap, bootstrap admin template, admin theme, admin dashboard, dashboard template, admin template, responsive" />
      <meta name="author" content="codedthemes" />
      <!-- Favicon icon -->

      <link rel="icon" href="../assets/images/favicon.ico" type="image/x-icon">
      <!-- Google font-->     
      <link href="https://fonts.googleapis.com/css?family=Roboto:400,500" rel="stylesheet">
      <!-- Required Fremwork -->
      <link rel="stylesheet" type="text/css" href="../assets/css/bootstrap/css/bootstrap.min.css">
      <!-- waves.css -->
      <link rel="stylesheet" href="../assets/pages/waves/css/waves.min.css" type="text/css" media="all">
      <!-- themify-icons line icon -->
      <link rel="stylesheet" type="text/css" href="../assets/icon/themify-icons/themify-icons.css">
      <!-- ico font -->
      <link rel="stylesheet" type="text/css" href="../assets/icon/icofont/css/icofont.css">
      <!-- Font Awesome -->
      <link rel="stylesheet" type="text/css" href="../assets/icon/font-awesome/css/font-awesome.min.css">
      <!-- Style.css -->
      <link rel="stylesheet" type="text/css" href="../assets/css/style.css">
  </head>

  <body themebg-pattern="theme1">
  <!-- Pre-loader start -->
  <div class="theme-loader">
      <div class="loader-track">
          <div class="preloader-wrapper">
              <div class="spinner-layer spinner-blue">
                  <div class="circle-clipper left">
                      <div class="circle"></div>
                  </div>
                  <div class="gap-patch">
                      <div class="circle"></div>
                  </div>
                  <div class="circle-clipper right">
                      <div class="circle"></div>
                  </div>
              </div>
              <div class="spinner-layer spinner-red">
                  <div class="circle-clipper left">
                      <div class="circle"></div>
                  </div>
                  <div class="gap-patch">
                      <div class="circle"></div>
                  </div>
                  <div class="circle-clipper right">
                      <div class="circle"></div>
                  </div>
              </div>
            
              <div class="spinner-layer spinner-yellow">
                  <div class="circle-clipper left">
                      <div class="circle"></div>
                  </div>
                  <div class="gap-patch">
                      <div class="circle"></div>
                  </div>
                  <div class="circle-clipper right">
                      <div class="circle"></div>
                  </div>
              </div>
            
              <div class="spinner-layer spinner-green">
                  <div class="circle-clipper left">
                      <div class="circle"></div>
                  </div>
                  <div class="gap-patch">
                      <div class="circle"></div>
                  </div>
                  <div class="circle-clipper right">
                      <div class="circle"></div>
                  </div>
              </div>
          </div>
      </div>
  </div>
  <!-- Pre-loader end -->
  <section class="login-block">
        <!-- Container-fluid starts -->
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-12">
                    <form class="md-float-material form-material" id="frmRegister" runat="server">
                        <div class="text-center">
                            <img src="../UploadFiles/2.jpg"  alt="logo.png" style="max-width:80px;max-height:50px;">
                        </div>
                        <div class="auth-box card">
                            <div class="card-block">
                                <div class="row m-b-20">
                                    <div class="col-md-12">
                                        <h3 class="text-center txt-primary">Sign up</h3>
                                    </div>
                                </div>
                                <div class="form-group form-primary">
                                    <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" required=""></asp:TextBox>
                                    <span class="form-bar"></span>
                                    <label class="float-label">Enter Username (First Name Last Name/Middle Name)</label>
                                </div>


                                    


                                <div class="form-group form-primary">
                                     <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" required=""></asp:TextBox>
                                    <span class="form-bar"></span>
                                    <label class="float-label">Your Email Address</label>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group form-primary">
                                                         <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" required="" TextMode="Password"></asp:TextBox>
                                            <span class="form-bar"></span>
                                            <label class="float-label">Password</label>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group form-primary">
                                                          <asp:TextBox ID="txtPasswordConfirm" runat="server" CssClass="form-control" required=""></asp:TextBox>
                                            <span class="form-bar"></span>
                                            <label class="float-label">Confirm Password</label>
                                        </div>
                                           <span class="form-bar"></span>
                                            <label class="float-label">
                                         <asp:CompareValidator ID="CompareValidator1" runat="server" 
     ControlToValidate="txtPasswordConfirm"
     CssClass="txt"
     ControlToCompare="txtPassword"
     ErrorMessage="No Match" 
     ToolTip="Password must be the same" />
                                                </label>
                                    </div>
                                </div>

                               
                                         <div class="form-group form-primary">
                                <asp:DropDownList ID="drpCollege" runat="server" CssClass ="form-control drp" required="">
                <asp:ListItem Text="Your College/Institution" Value="-1"></asp:ListItem>
                      <asp:ListItem Text="Amar Singh College" Value="1"></asp:ListItem>
                <asp:ListItem Text="Gandhi Memorial College" Value="2"></asp:ListItem>
                <asp:ListItem Text="Govt. Degree College, Pulwama" Value="3"></asp:ListItem>
                <asp:ListItem Text="Govt Degree College, Kulgam" Value="4"></asp:ListItem>
                <asp:ListItem Text="Govt. Degree College, Baramulla" Value="5"></asp:ListItem>
                       <asp:ListItem Text="ICSC, University of Kashmir" Value="6"></asp:ListItem>
                          <asp:ListItem Text="SP College" Value="7"></asp:ListItem>
                 <asp:ListItem Text="Womens College MA Road" Value="8"></asp:ListItem>
             
            </asp:DropDownList>
            
                                    <span class="form-bar"></span>
                                    <label class="float-label">
                                        <asp:RequiredFieldValidator ID="rfvCollege" runat="server" CssClass="lbl" ErrorMessage="Select College in the list" InitialValue="-1"
                ControlToValidate="drpCollege" Display="Dynamic" ValidationGroup="SubmitUserInfo"></asp:RequiredFieldValidator>
                                    </label>
                                </div>
  <div class="row">
                                    <div class="col-sm-6">
    <div class="form-group form-primary">
             <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control" required=""></asp:TextBox>
                                    <span class="form-bar"></span>
                                    <label class="float-label">Your Designation</label>
        </div>

    </div>
   <div class="col-sm-6">
        <div class="form-group form-primary">
              <asp:DropDownList ID="drpConvenorRole" runat="server" CssClass="form-control drp" required="">
                <asp:ListItem Text="Your Convenor Role" Value="-1"></asp:ListItem>
                     <asp:ListItem Text="Divisional Head" Value="1"></asp:ListItem>
                     <asp:ListItem Text="Psychological Divisional Coordinator" Value="2"></asp:ListItem>
                     <asp:ListItem Text="Carrer Divisional Coordinator" Value="3"></asp:ListItem>
                     <asp:ListItem Text="Placement Divisional Coordinator" Value="4"></asp:ListItem>
                     <asp:ListItem Text="District Coordinator" Value="5"></asp:ListItem>
                     <asp:ListItem Text="College Coordinator" Value="6"></asp:ListItem>
            </asp:DropDownList>
         
               <span class="form-bar"></span>
                                    <label class="float-label">
                                           <asp:RequiredFieldValidator ID="rfvConvenorRole" runat="server" CssClass="lbl" ErrorMessage="<b>Select Convenor Role in the list</b>" InitialValue="-1"
                ControlToValidate="drpConvenorRole" Display="Dynamic" ValidationGroup="SubmitUserInfo"></asp:RequiredFieldValidator>
                                    </label>
        </div>
       

    </div>
      </div>
                                      <div class="row m-t-25 text-left">
                                    <div class="col-md-12">
                                        <div class="checkbox-fade fade-in-primary">
                                            <label>
                                                <input type="checkbox" value="" required="" id="chkTerms" runat="server">

                                                <span class="cr"><i class="cr-icon icofont icofont-ui-check txt-primary"></i></span>
                                                <span class="text-inverse">* I read and accept <a href="#">Terms &amp; Conditions.</a></span>
                                            </label>
                                        </div>
                                    </div>
                                </div>

  <div class="row m-t-30">
                                    <div class="col-md-12">
            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary btn-md btn-block waves-effect text-center m-b-20" Text="Sign Up" ValidationGroup="SubmitUserInfo" OnClick="btnSubmit_Click" />
            <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="Cancel" style="display:none;" OnClick="btnCancel_Click" />
        </div>

    </div>

                                <hr/>
                                <div class="row">
                                    <div class="col-md-10">
                                        <p class="text-inverse text-left"><a href="Login.aspx"><b>Sign In</b></a></p>
                                    </div>
                                    <div class="col-md-2">
                                        <img src="../UploadFiles/2.jpg"   style="max-width:50px;max-height:30px;"  alt="small-logo.png">
                                    </div>
                                </div>
                            </div>
                        </div>
                           <asp:Label ID="lblmsg" runat="server"></asp:Label>
                         <style type="text/css">
        .txt {
            text-align: left;
            margin-top: 5px;
        }

        .lbl {
            color: Red;
            font-weight: bolder;
        }
                             </style>
                    </form>

                </div>
                <!-- end of col-sm-12 -->
            </div>
            <!-- end of row -->
        </div>
        <!-- end of container-fluid -->
    </section>
    <!-- Warning Section Starts -->
    <!-- Older IE warning message -->
    <!--[if lt IE 10]>
<div class="ie-warning">
    <h1>Warning!!</h1>
    <p>You are using an outdated version of Internet Explorer, please upgrade <br/>to any of the following web browsers to access this website.</p>
    <div class="iew-container">
        <ul class="iew-download">
            <li>
                <a href="http://www.google.com/chrome/">
                    <img src="../assets/images/browser/chrome.png" alt="Chrome">
                    <div>Chrome</div>
                </a>
            </li>
            <li>
                <a href="https://www.mozilla.org/en-US/firefox/new/">
                    <img src="../assets/images/browser/firefox.png" alt="Firefox">
                    <div>Firefox</div>
                </a>
            </li>
            <li>
                <a href="http://www.opera.com">
                    <img src="../assets/images/browser/opera.png" alt="Opera">
                    <div>Opera</div>
                </a>
            </li>
            <li>
                <a href="https://www.apple.com/safari/">
                    <img src="../assets/images/browser/safari.png" alt="Safari">
                    <div>Safari</div>
                </a>
            </li>
            <li>
                <a href="http://windows.microsoft.com/en-us/internet-explorer/download-ie">
                    <img src="../assets/images/browser/ie.png" alt="">
                    <div>IE (9 & above)</div>
                </a>
            </li>
        </ul>
    </div>
    <p>Sorry for the inconvenience!</p>
</div>
<![endif]-->
<!-- Warning Section Ends -->
<!-- Required Jquery -->
    <script type="text/javascript" src="../assets/js/jquery/jquery.min.js"></script>     <script type="text/javascript" src="../assets/js/jquery-ui/jquery-ui.min.js "></script>     <script type="text/javascript" src="assets/js/popper.js/popper.min.js"></script>     <script type="text/javascript" src="assets/js/bootstrap/js/bootstrap.min.js "></script>
<!-- waves js -->
<script src="../assets/pages/waves/js/waves.min.js"></script>
<!-- jquery slimscroll js -->
<script type="text/javascript" src="../assets/js/jquery-slimscroll/jquery.slimscroll.js "></script>
<!-- modernizr js -->
    <script type="text/javascript" src="../assets/js/SmoothScroll.js"></script>     <script src="../assets/js/jquery.mCustomScrollbar.concat.min.js "></script>
<!-- i18next.min.js -->
<script type="text/javascript" src="bower_components/i18next/js/i18next.min.js"></script>
<script type="text/javascript" src="bower_components/i18next-xhr-backend/js/i18nextXHRBackend.min.js"></script>
<script type="text/javascript" src="bower_components/i18next-browser-languagedetector/js/i18nextBrowserLanguageDetector.min.js"></script>
<script type="text/javascript" src="bower_components/jquery-i18next/js/jquery-i18next.min.js"></script>
<script type="text/javascript" src="../assets/js/common-pages.js"></script>
</body>

</html>
