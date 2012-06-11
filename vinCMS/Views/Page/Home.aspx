<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Domain.Entities.Page>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Barbarian Meets Coding</title>

    <meta name="google-site-verification" content="o_Vc3itm6Yl8LH0wKI_MPOfK2rfUyt8tRwe5lDAu9wQ" />

    <link rel="stylesheet" href="~/public/css/screen.css" type="text/css" media="screen, projection" />
    <link rel="stylesheet" href="~/public/css/print.css" type="text/css" media="print" />
<!--[if IE]><link rel="stylesheet" href="ie.css" type="text/css" media="screen, projection" /><![endif]-->
        <!--- Import fancy-type plugin --->
    <link rel="stylesheet" href="~/public/css/plugins/fancy-type/screen.css" type="text/css" />
    <link rel="stylesheet" href="~/public/css/plugins/buttons/screen.css" type="text/css" />
    <!--- Import custom styles --->
    <link rel="stylesheet" href="~/public/css/home.css" type="text/css" />
    <link rel="SHORTCUT ICON" href="/favicon.ico"/>
</head>
<body>
    <%: Html.DisplayForModel() %>
    <div id="home-navbar" class="container">
        <div class="span-15 last">
            <% Html.RenderAction("NavBar", "Navigation"); %>
        </div>
    </div>
    <div>
        <!-- Google Analytics -->
        <script type="text/javascript">

            var _gaq = _gaq || [];
            _gaq.push(['_setAccount', 'UA-19776656-1']);
            _gaq.push(['_trackPageview']);

            (function () {
                var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
                ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
                var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
            })();

          </script>
    </div>
</body>
</html>
