<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="eCollectionMenuForGroups.ascx.cs" Inherits="DotNetNuke.Modules.eCollection_Groups.Groups.eCollectionMenuForGroups" %>

<div style="width: 100%; float: left; margin-top: 20px;">
    <div id="DashboardTabHolder" style="width: 100%; float: left;">
        <div id="DashboardTab" class="MenuItems" style="background-position: 0 -191px;">
            DASHBOARD
        </div>
    </div>
    <div id="StudentsTabHolder" style="width: 100%; float: left;">
        <div id="StudentsTab" class="MenuItems" style="background-position: 0 -95px;">
            STUDENTS
        </div>
    </div>
    <div id="GroupsTabHolder" style="width: 100%; float: left;">
        <div id="GroupsTab" class="MenuItems" style="background-position: 0 -241px;">
            GROUPS
        </div>
    </div>
    <div id="SessionTabHolder" style="width: 100%; float: left;">
        <div id="SessionTab" class="MenuItems" style="background-position: 0 -143px;">
            SESSIONS
        </div>
    </div>
    <div id="BooksTabHolder" style="width: 100%; float: left;">
        <div id="BooksTab" class="MenuItems">
            BOOKS
        </div>
    </div>
    <div id="TeachersTabHolder" style="width: 100%; float: left;">
        <div id="TeachersTab" class="MenuItems" style="background-position: 0 -43px;">
            TEACHERS
        </div>
    </div>
</div>

<div style="float: right; width: 100%; margin-top: 20px; text-align: right;">
    <hr style="height: 1px; background-color: lightgray; float: right; width: 90%; border: 0px solid lightgray;" />
</div>
<script type="text/javascript">
    jQuery(function () {
        jQuery("#DashboardTab").click(function () { window.location.href = "/dotnetnuke/cengage/eCollection.aspx"; });
        jQuery("#StudentsTab").click(function () { window.location.href = "/dotnetnuke/cengage/ecollection/Students.aspx"; });
        jQuery("#GroupsTab").click(function () { window.location.href = "/dotnetnuke/cengage/ecollection/Groups.aspx"; });
        jQuery("#SessionTab").click(function () { window.location.href = "/dotnetnuke/cengage/ecollection/Sessions.aspx"; });
        jQuery("#BooksTab").click(function () { window.location.href = "/dotnetnuke/cengage/ecollection/Books.aspx"; });
        jQuery("#TeachersTab").click(function () { window.location.href = "/dotnetnuke/cengage/ecollection/Teachers.aspx"; });
    });
</script>

