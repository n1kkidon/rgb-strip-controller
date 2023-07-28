package com.web.Api.FileUploader;


import jakarta.annotation.security.PermitAll;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(path = "/api/file")
@PermitAll
public class FileController {

    @GetMapping("/sv")
    public ResponseEntity<String> test(){
        return ResponseEntity.ok("zdrv");
    }

}
