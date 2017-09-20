
function initUserSelectric() {
    $('select[name=Seniority]').selectric({
        maxHeight: 200,
        responsive: true,
        preventWindowScroll: true,
        inheritOriginalWidth: true,
    });

    $('select[name=Role]').selectric({
        maxHeight: 200,
        responsive: true,
        preventWindowScroll: true,
        inheritOriginalWidth: true,
    });

    $('select[name=Technologies]').selectric({
        maxHeight: 200,
        responsive: true,
        preventWindowScroll: true,
        inheritOriginalWidth: true,
    });
}

function initContactSelectric() {
    $('select[name=Seniority]').selectric({
        maxHeight: 200,
        responsive: true,
        preventWindowScroll: true,
        inheritOriginalWidth: true,
    });

    $('select[name=Technologies]').selectric({
        maxHeight: 200,
        responsive: true,
        preventWindowScroll: true,
        inheritOriginalWidth: true,
    });
}

function addNewTechnology() {
    let newTechName = $('input[name=newTechName]').val();
    let exists = $('select[name=Technologies] > option').filter((ind, elem) => {
        return (elem.innerHTML === newTechName);
    });

    if (exists.length > 0)
    {
        exists.prop('selected', 'true');
        $('select[name=Technologies]').selectric('refresh');
    }
    else
    {
        $.ajax({
            url: '/Data/AddNewTechnology',
            type: 'POST',
            data: {
                'newTechName': newTechName
            },
            success: (response) => {
                $('select[name=Technologies]').append('<option value="' + response.newTechId + '" selected>' + newTechName + '</option>');
                $('select[name=Technologies]').selectric('refresh');
            }
        });
    }

    $('input[name=newTechName]').val('');
}

function selectCV() {
    $('#CV').click();
}

function writeCVInfo(e) {
    let path = e.value;

    let startIndex = (path.indexOf('\\') >= 0 ? path.lastIndexOf('\\') : path.lastIndexOf('/'));
    let fileName = path.substring(startIndex);
    if (fileName.indexOf('\\') === 0 || fileName.indexOf('/') === 0)
        fileName = fileName.substring(1);

    $('#CVFilename').html(fileName);
}