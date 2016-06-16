package com.grapecity.crypto.hash;

import org.apache.commons.cli.*;
import org.apache.shiro.crypto.hash.*;

import java.util.Arrays;

public class Main {

    public static void main(String[] args) throws ParseException {
        Options opts = new Options();
        opts.addOption("a", "algorithm", true, "algorithm to be used");
        opts.addOption("s", "source", true, "the source to compute hash");
        opts.addOption("t", "salt", true, "the salt");
        opts.addOption("i", "iterations", true, "iterations when compute hash");

        CommandLineParser parser = new DefaultParser();
        CommandLine cmd = parser.parse(opts, args);

        if(!cmd.hasOption("s")){
            System.out.println("need source for computing hash");
            System.exit(1);
        }
        String source = cmd.getOptionValue("s");

        String salt = null;
        if(cmd.hasOption("t")){
            salt = cmd.getOptionValue("t");
        }

        int iterations = 1;
        if(cmd.hasOption("i")){
            iterations = Integer.parseInt(cmd.getOptionValue("i"), 10);
        }
        if(iterations < 1){
            iterations = 1;
        }

        SimpleHash hash = null;
        String[] hashAlgorithms = new String[]{
                Md2Hash.ALGORITHM_NAME,
                Md5Hash.ALGORITHM_NAME,
                Sha1Hash.ALGORITHM_NAME,
                Sha256Hash.ALGORITHM_NAME,
                Sha384Hash.ALGORITHM_NAME,
                Sha512Hash.ALGORITHM_NAME
        };
        if(cmd.hasOption("a")){
            String alg = cmd.getOptionValue("a");
            if(Arrays.asList(hashAlgorithms).contains(alg)){
                hash = new SimpleHash(alg, source, salt, iterations);
            }
        }
        if(hash == null){
            hash = new Sha1Hash(source, salt, iterations);
        }

        System.out.println("algorithms  :" + hash.getAlgorithmName());
        System.out.println("source      :" + source);
        System.out.println("salt        :" + salt);
        System.out.println("iterations  :" + iterations);
        System.out.println("hash(hex)   :" + hash.toHex());
        System.out.println("hash(base64):" + hash.toBase64());
    }
}
