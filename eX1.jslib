var HogePlugin = {
    $funcs: {},

    Init: function (piyoPtr) {
        funcs.piyoPtr = piyoPtr;
    },

    Fuga: function (piyoPtr) {
        var str = '日本語';
        var intVal = 123;

        var encoder = new TextEncoder();
        var strBuffer = encoder.encode(str + String.fromCharCode(0)); // 文字列はnull文字終端にする
        var strPtr = _malloc(strBuffer.length);
        HEAP8.set(strBuffer, strPtr);
        // C#のPiyo関数を呼ぶ
        Runtime.dynCall('vii', funcs.piyoPtr, [strPtr, intVal]);
        _free(strPtr);
    }
};
autoAddDeps(HogePlugin, '$funcs');
mergeInto(LibraryManager.library, HogePlugin);