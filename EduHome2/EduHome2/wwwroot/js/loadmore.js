let skipCount = 6


$(document).on("click", "#loadMoreBtn", function () {
    $.ajax({
        url: "/Courses/LoadMore/",
        type: "GET",
        data: {
            "skip":skipCount
            },
        success: function (response) {
            $("#myCourses").append(response)
            skipCount += 6;
            if ($("#coursesCount").val() <= skipCount) {
                $("#loadMoreBtn").remove()
            }

            if (response == "stop") {
                $("html").remove()
                alert("get")
            }
        }
    });
});