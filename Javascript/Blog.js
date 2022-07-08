function CreateComment(CommentId) {
    {
        try {
            $.ajax({
                url: "Comments/Create",
                type: "POST",
                data: {
                    id: CommentId,
                },
                success: function (result) {

                    $('#mycomments').reload()
                },
                error: function (error) {
                    alert(error);
                }
            })
        }
        catch (e) {
            alert(e.message);
        }
    }
}


function clickLike(CommentId) {
    {
        try {
            $.ajax({
                url: "/Comments/GetLike",
                type: "POST",
                data: { id: CommentId },
                success: function (result) {
                    //alert(result.Data.Likes)
                    $("div").find('p[id="likevalue ' + CommentId + '"]').text(result.Data.Likes);
                    ldratio(CommentId);
                },
                error: function (error) {
                    alert(error);
                }
            })
        }
        catch (e) {
            alert(e.message);
        }
    }
}

function clickDislike(CommentId) {
    {
        try {
            $.ajax({
                url: "/Comments/GetDislike",
                type: "POST",
                data: { id: CommentId },
                success: function (result) {
                    $("div").find('p[id="dislikevalue ' + CommentId + '"]').text(result.Data.Dislikes);
                    ldratio(CommentId);
                },
                error: function (error) {
                    alert(error);
                }
            })
        }
        catch (e) {
            alert(e.message);
        }
    }
}

function ldratio(CommentId) {
    {
        try {
            $.ajax({
                url: "/Comments/ldratiobar",
                type: "POST",
                data: { id: CommentId },
                success: function (result) {
                    var percent = result.Data + "%";
                    var dislikepercent = (100 - result.Data + '% dislikes')
                    document.getElementById('progbar ' + CommentId).style.width = percent;
                    $('label[class="like-ratio ' + CommentId + '"]').text(result.Data + '% likes' + " | " + dislikepercent )
                },
                error: function (error) {
                    alert(error);
                }
            })
        }
        catch (e) {
            alert(e.message);
        }
    }
}



function commentdelete(CommentId) {
    {
        try {
            $.ajax({
                url: '/Comments/DeleteConfirmed',
                type: "POST",
                data: { id: CommentId },
                success: function (result) {
                    
                    console.log("Item" + CommentId);
                    $('div[id="deletesuccess"]').fadeOut("slow", function () {
                        $('div[id="commentbox ' + CommentId + '"]').replaceWith($('div[id="deletesuccess"]'));
                        $('#deletesuccess').css('opacity', '1')
                        $('div[id="deletesuccess"]').fadeIn("slow").delay(3000).fadeOut("slow")
                        
                    })
                    
                     
                    $('.modal').modal('hide');
                    $('div[class="modal ' + CommentId + '"]').remove();
                },
                error: function (error) {
                    alert(error);
                }
            })
        }
        catch (e) {
            alert(e.message);
        }
    }
}



