use std::env;
use std::fs::File;
use std::io::{self, BufRead};
use std::path::Path;

fn main() {
    let args: Vec<String> = env::args().collect();
    let file_path = &args[1];

    let mut total = 0;

    if let Ok(lines) = read_lines(file_path) {
        // Consumes the iterator, returns an (Optional) String
        for line in lines {
            if let Ok(ip) = line {
                let mut first = 10;
                let mut last = 10;
                for c in ip.chars() {
                    if c.is_digit(10) {
                        if first == 10 {
                            first = c.to_digit(10).unwrap();
                        } else {
                            last = c.to_digit(10).unwrap();
                        }
                    }
                }
                if last == 10 {
                    last = first;
                }
                println!("{first}{last}");
                total += format!("{first}{last}").parse::<i32>().unwrap();
            }
        }
    }
    println!("total {}", total);
}

fn read_lines<P>(filename: P) -> io::Result<io::Lines<io::BufReader<File>>>
where
    P: AsRef<Path>,
{
    let file = File::open(filename)?;
    Ok(io::BufReader::new(file).lines())
}
